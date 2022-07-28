using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class choose : MonoBehaviour
{
    // Start is called before the first frame update
    public void switchScene()
    {
        Stateholder.curr_poi = int.Parse(this.name);
        SceneManager.LoadScene("SearchGPS");
    }
}
