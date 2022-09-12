using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Tutorial_Title : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetString("finished", "false"); //vor Abgabe rausnehmen

        if (PlayerPrefs.GetString("finished") == "true")
        {
            panel.SetActive(false);
        }
    }

    public void hidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    // public void skipTutorial(bool skip)
    // {

    //     if (skip)
    //     {
    //         PlayerPrefs.SetString("finished", "true");
    //         gameObject.SetActive(false);
            
    //     }
    // }
    public void end_Tutorial(GameObject panel)
    {
        // if(SceneManager.GetActiveScene().name == "")
        // {
            PlayerPrefs.SetString("finished", "true");
            panel.SetActive(false);
        // }
    }
    
    public void start_Tutorial(GameObject panel)
    {

        Debug.Log("Tutorial gestartet");
        PlayerPrefs.SetString("finished", "false");
        panel.SetActive(true);
    }
}
