using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GeoCoordinatePortable;

public class GPScontroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Distance;
    [SerializeField] private TextMeshProUGUI poiname;
    [SerializeField] private TextMeshProUGUI countname;
    [SerializeField] private RawImage compass_img;
    [SerializeField] private Button found_btn;
    [SerializeField] private Button hide_btn;

    private bool inradius;
    private double distance = 0;
    private RectTransform compass_rect;

    private float _headingVelocity = 0f;
    // Start is called before the first frame update
    void Start()
    {
        compass_rect = compass_img.GetComponent<RectTransform>();
        inradius = false;
        found_btn.interactable = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        poiname.text = Stateholder.pois[Stateholder.curr_poi].name;
    }

    void FixedUpdate()
    {
        if (distance <= Stateholder.radius)
        {
            inradius = true;
            found_btn.interactable = true;
            hide_btn.interactable = true;
            compass_img.enabled = false;
        }
        else
        {
            inradius = false;
            found_btn.interactable = false;
            hide_btn.interactable = false;
            compass_img.enabled = true;
        }
    }

    void Update()
    {

        if (Stateholder.pois[Stateholder.curr_poi].following != null)
        {

            countname.text = (Stateholder.curr_schnitzel_counter + 1) + "/" + (Stateholder.pois[Stateholder.curr_poi].following.Length + 1);
        }
        else
        {
            countname.text = "";
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Failed GPS");
        }
        else
        {

            GeoCoordinate curr_pos = new GeoCoordinate(Input.location.lastData.latitude, Input.location.lastData.longitude);
            GeoCoordinate wanna_be = new GeoCoordinate(Stateholder.pois[Stateholder.curr_poi].coordinates[0], Stateholder.pois[Stateholder.curr_poi].coordinates[1]);
            distance = curr_pos.GetDistanceTo(wanna_be);
            Quaternion compass = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);

            float angle = Mathf.SmoothDampAngle((int)(getBearing(curr_pos, wanna_be) - compass.eulerAngles.y), Input.compass.trueHeading, ref _headingVelocity, 0.1f);

            compass_rect.SetPositionAndRotation(compass_rect.transform.position, Quaternion.Euler(60, 0, angle - 90));

        }
        Distance.text = inradius ? "<" + Stateholder.radius + "m" : ((int)distance).ToString() + "m";
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