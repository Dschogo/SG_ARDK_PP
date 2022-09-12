using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inv_filler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject content_go;
    [SerializeField] private GameObject inv_item;
    void Start()
    {

        for (int i = 0; i < Stateholder.item_count; i++)
        {
            GameObject go = Instantiate(inv_item) as GameObject;

            go.name = i.ToString();
            go.transform.SetParent(content_go.transform);
            go.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture2D>("InvImg/" + i);
            if (Stateholder.found_items.Contains(i))
                go.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 1f);
            else
            {
                go.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.1f);
            }
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
