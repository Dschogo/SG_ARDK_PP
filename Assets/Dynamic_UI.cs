using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine.UI;

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
        entry.GetComponentsInChildren<TextMeshProUGUI>()[1].text = poi.coordinates[0].ToString() + "\n" + poi.coordinates[1].ToString();


        var bytes = System.Convert.FromBase64String(poi.image);
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(bytes);
        tex.Apply();

        entry.GetComponentsInChildren<RawImage>()[0].texture = tex;


    }

    void Update()
    {
        if (fechtedpoi)
        {
            text.text = "";
            if (buildodui)
            {
                // just update disctance here  reodering possible?
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
}
