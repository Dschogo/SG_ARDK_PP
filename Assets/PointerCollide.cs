using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollide : MonoBehaviour
{
    private int ItemToGet;
    // Start is called before the first frame update
    void Start()
    {
        ItemToGet = Random.Range(0, Stateholder.item_count);
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
    }
}