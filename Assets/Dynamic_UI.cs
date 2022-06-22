using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class POI
{
    public string name;
    public float[] coordinates;
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
    [SerializeField] private GameObject obj;

    private POI[] pois;
    private bool fechtedpoi = false;
    private bool buildodui = false;
    void Start()
    {
        StartCoroutine(GetRequest("https://pastebin.com/raw/fp0dVQnw"));

    }

    void Update()
    {
        if (fechtedpoi)
        {
            text.text = "";
            if (fechtedpoi)
            {
                // just update disctance here  reodering possible?
            }
            else
            {
                for (int i = 0; i < pois.Length; i++)
                {
                    string name = pois[i].name;
                    float[] coordinates = pois[i].coordinates;
                }
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
                    if (pages[page] == "fp0dVQnw")
                    {
                        this.fechtedpoi = true;
                        POIS pois = JsonUtility.FromJson<POIS>(webRequest.downloadHandler.text);
                    }
                    break;
            }
        }
    }
}
