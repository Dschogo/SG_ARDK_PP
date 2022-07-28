using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backbtn : MonoBehaviour
{
    void Start()
    {
        Screen.fullScreen = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Inv":
                    SceneManager.LoadScene("Title");
                    break;
                case "Main":
                    SceneManager.LoadScene("Title");
                    break;
                case "SearchGPS":
                    SceneManager.LoadScene("Main");
                    break;
                case "VPSCreate":
                    SceneManager.LoadScene("SearchGPS");
                    break;
                default:
                    Application.Quit();
                    break;
            }
        }
    }
}
