using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class image : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    private bool big = false;
    private Vector3 my_transform;
    // Start is called before the first frame update
    void Start()
    {
        RawImage img = GetComponent<RawImage>();
        my_transform = this.transform.position;
        if (Stateholder.pois[Stateholder.curr_poi].image != null && Stateholder.pois[Stateholder.curr_poi].image != "")
        {
            try
            {
                byte[] bytes = System.Convert.FromBase64String(Stateholder.pois[Stateholder.curr_poi].image.Split(',')[1]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(bytes);
                tex.Apply();

                img.texture = tex;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
        else
        {
            img.enabled = false;
        }

    }

    public void make_big()
    {
        if (big)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.position = my_transform;
            big = false;
        }
        else
        {
            this.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            this.transform.position = canvas.transform.position;
            big = true;
        }
    }
}
