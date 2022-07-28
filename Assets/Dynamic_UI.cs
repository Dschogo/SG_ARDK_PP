using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine.UI;
using GeoCoordinatePortable;

[System.Serializable]
public class POI
{
    public string name;
    public float[] coordinates;

    public string image;
}


[System.Serializable]
public class POIS
{
    //employees is case sensitive and must match the string "employees" in the JSON.
    public POI[] pois;
}
public class Dynamic_UI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private GameObject contents;
    [SerializeField] private GameObject prefab;


    private POI[] pois;
    private bool fechtedpoi = false;
    private bool buildodui = false;


    void Start()
    {
        //StartCoroutine(GetRequest("https://pastebin.com/raw/fp0dVQnw"));
        StartCoroutine(GetRequest("https://raw.githubusercontent.com/Dschogo/SG_ARDK_PP/main/Assets/POI.json"));

    }

    void create_entry(POI poi, int i)
    {
        GameObject entry = Instantiate(prefab, contents.transform);
        entry.name = i.ToString();
        entry.GetComponentsInChildren<TextMeshProUGUI>()[0].text = poi.name;

        GeoCoordinate curr_pos = new GeoCoordinate(Input.location.lastData.latitude, Input.location.lastData.longitude);
        GeoCoordinate wanna_be = new GeoCoordinate(poi.coordinates[0], poi.coordinates[1]);

        entry.GetComponentsInChildren<TextMeshProUGUI>()[1].text = curr_pos.GetDistanceTo(wanna_be).ToString("F2") + "m";

        if (poi.image != null)
        {
            try
            {
                byte[] bytes = System.Convert.FromBase64String(poi.image.Split(',')[1]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(bytes);
                tex.Apply();

                entry.GetComponentsInChildren<RawImage>()[0].texture = tex;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

        }




    }

    void Update()
    {
        if (fechtedpoi)
        {
            text.text = "";
            if (buildodui)
            {
                for (int i = 0; i < pois.Length; i++)
                {
                    GeoCoordinate curr_pos = new GeoCoordinate(Input.location.lastData.latitude, Input.location.lastData.longitude);
                    GeoCoordinate wanna_be = new GeoCoordinate(pois[i].coordinates[0], pois[i].coordinates[1]);
                    RawImage arrow = contents.transform.GetChild(i).GetComponentsInChildren<RawImage>()[1];

                    if (curr_pos.GetDistanceTo(wanna_be) < Stateholder.radius)
                    {
                        contents.transform.GetChild(i).GetComponentsInChildren<TextMeshProUGUI>()[1].text = "<" + Stateholder.radius.ToString() + "m";
                        arrow.enabled = false;

                    }
                    else
                    {
                        contents.transform.GetChild(i).GetComponentsInChildren<TextMeshProUGUI>()[1].text = curr_pos.GetDistanceTo(wanna_be).ToString("F2") + "m";
                        arrow.enabled = true;
                        Quaternion compass = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);

                        int angle = (int)(getBearing(curr_pos, wanna_be) - compass.eulerAngles.y);

                        arrow.transform.SetPositionAndRotation(arrow.transform.position, Quaternion.Euler(0, 0, angle - 90));
                    }

                }
            }
            else
            {

                for (int i = 0; i < pois.Length; i++)
                {
                    create_entry(pois[i], i);
                }
                Stateholder.pois = pois;
                buildodui = true;
            }
        }
        else
        {
            text.text = "not fetched yet";
        }
    }

    // Update is called once per frame
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    //if (pages[page] == "fp0dVQnw")
                    this.fechtedpoi = true;
                    pois = JsonUtility.FromJson<POIS>(webRequest.downloadHandler.text).pois;

                    break;
            }
        }
    }
    private double getBearing(GeoCoordinate curr, GeoCoordinate wanna_be)
    {
        // https://stackoverflow.com/questions/64791543/finding-a-heading-towards-a-coordinate
        float deltaL = (float)(ToRadians(wanna_be.Longitude) - ToRadians(curr.Longitude));
        float thataB = (float)ToRadians(wanna_be.Latitude);
        float thataA = (float)ToRadians(curr.Latitude);
        float x = (float)(Mathf.Cos(thataB) * Mathf.Sin(deltaL));
        float y = (float)(Mathf.Cos(thataA) * Mathf.Sin(thataB) - Mathf.Sin(thataA) * Mathf.Cos(thataB) * Mathf.Cos(deltaL));
        float bearing = (float)Mathf.Atan2(x, y);
        return ToDegrees(bearing);
    }

    private static double ToRadians(double val)
    {
        return (Mathf.PI / 180) * val;
    }
    private static double ToDegrees(double radians)
    {
        return (180 / Mathf.PI) * radians;
    }
}
