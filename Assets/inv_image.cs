using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inv_image : MonoBehaviour
{

    private Canvas canvas;
    private bool big = false;
    private Vector3 my_transform;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
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
            my_transform = this.transform.position;
            this.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            this.transform.position = canvas.transform.position;
            big = true;
        }
    }
}
