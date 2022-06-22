using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchLevel(string Level)
    {
        Debug.Log("fuzck ui");
        SceneManager.LoadScene(Level);
    }
}
