using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stateholder : MonoBehaviour
{
    public static int curr_poi;
    public static POI[] pois;

    public static int radius = 25; //in meters

    public static int curr_schnitzel_counter = 0;

    public static List<int> found_items = new List<int>();

    public static int item_count;

    public static void init()
    {
        item_count = Resources.LoadAll<Texture2D>("InvImg").Length;
        Debug.Log(item_count);
        if (PlayerPrefs.HasKey("found_items"))
        {

            string[] found_items_string_arr = PlayerPrefs.GetString("found_items").Split(',');
            for (int i = 0; i < found_items_string_arr.Length; i++)
            {
                found_items.Add(int.Parse(found_items_string_arr[i]));
            }
        }

    }

    public static void save_found_items()
    {
        PlayerPrefs.SetString("found_items", string.Join(",", found_items));
    }

}