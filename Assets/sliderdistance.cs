using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sliderdistance : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();
        text.text = slider.value.ToString() + "m";
    }
    // Update is called once per frame
    public void OnValueChanged()
    {
        text.text = slider.value.ToString() + "m";
        Stateholder.radius = (int)slider.value;
    }
}
