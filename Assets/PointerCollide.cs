using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PointerCollide : MonoBehaviour
{
    private int ItemToGet;

    [SerializeField] private RawImage image;

    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        ItemToGet = Random.Range(0, Stateholder.item_count);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Stateholder.found_items.Add(ItemToGet);
        Stateholder.save_found_items();
        Debug.Log("Collided with Anchor");

        image.texture = Resources.Load<Texture2D>("InvImg/" + ItemToGet);
        panel.SetActive(true);

        //popupo


    }


    public void do_da_clickin()
    {
        // popup click
        if (Stateholder.pois[Stateholder.curr_poi].following != null) {
            if (Stateholder.curr_schnitzel_counter < Stateholder.pois[Stateholder.curr_poi].following.Length - 1)
            {
                Stateholder.curr_schnitzel_counter++;

                //to next map 
                SceneManager.LoadScene("SearchGPS");

                return;
            }
            else
            {
                Stateholder.curr_schnitzel_counter = 0;

            }
        }
        else
        {
            Stateholder.curr_schnitzel_counter = 0;
        }
        
        SceneManager.LoadScene("Inv");

        // to inv
    }
}