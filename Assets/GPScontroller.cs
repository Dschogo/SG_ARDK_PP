using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GeoCoordinatePortable;

public class GPScontroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Cords;
    [SerializeField] private TextMeshProUGUI Distance;
    [SerializeField] private TextMeshProUGUI rotat;
    [SerializeField] private RawImage compass_img;
    [SerializeField] private Button found_btn;

    [SerializeField] private int Radius;


    private bool inradius;
    private double distance = 20;
    private RectTransform compass_rect;
    private int angle;
    // Start is called before the first frame update
    void Start()
    {
        compass_rect = compass_img.GetComponent<RectTransform>();
        inradius = false;
        found_btn.interactable = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void FixedUpdate()
    {
        if (distance <= Radius)
        {
            inradius = true;
            found_btn.interactable = true;
        }
        else
        {
            inradius = false;
            found_btn.interactable = false;
        }
    }

    void Update()
    {

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Cords.text = "Unable to determine device location";
        }
        else
        {
            Cords.text = "Latitude: " + Input.location.lastData.latitude + " Longitude: " + Input.location.lastData.longitude;

            GeoCoordinate curr_pos = new GeoCoordinate(Input.location.lastData.latitude, Input.location.lastData.longitude);
            GeoCoordinate wanna_be = new GeoCoordinate(49.86086629274778f, 8.566904834576274f);
            distance = curr_pos.GetDistanceTo(wanna_be);
            Quaternion compass = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);

            angle = (int)(getBearing(curr_pos, wanna_be) - compass.eulerAngles.y);
            compass_rect.SetPositionAndRotation(compass_rect.transform.position, Quaternion.Euler(0, 0, angle - 90));

            rotat.text = compass.eulerAngles.y.ToString() + " : " + getBearing(curr_pos, wanna_be).ToString() + " : " + angle.ToString() + " : " + Input.compass.headingAccuracy.ToString(); ;
        }
        Distance.text = "Distance: " + ((int)distance).ToString() + "m";
        found_btn.interactable = inradius;
    }


    private static double ToRadians(double val)
    {
        return (Mathf.PI / 180) * val;
    }
    private static double ToDegrees(double radians)
    {
        return (180 / Mathf.PI) * radians;
    }

    private double getBearing(GeoCoordinate curr, GeoCoordinate wanna_be)
    {
        // https://stackoverflow.com/questions/64791543/finding-a-heading-towards-a-coordinate
        float deltaL = (float)(ToRadians(wanna_be.Longitude) - ToRadians(curr.Longitude));
        float thataB = (float)ToRadians(wanna_be.Latitude);
        float thataA = (float)ToRadians(curr.Latitude);
        float x = (float)(Mathf.Cos(thataB) * Mathf.Sin(deltaL));
        float y = (float)(Mathf.Cos(thataA) * Mathf.Sin(thataB) - Mathf.Sin(thataA) * Mathf.Cos(thataB) * Mathf.Cos(deltaL));
        float bearing = (float)Mathf.Atan2(x, y);
        return ToDegrees(bearing);
    }


}