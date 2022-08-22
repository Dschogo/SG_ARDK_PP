using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Tutorial_Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
       if(PlayerPrefs.GetString("finished") == "true")
        {
           
        }
    }
    void skipTutorial(bool skip)
    {

        if (skip)
        {
            PlayerPrefs.SetString("finished", "true");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
