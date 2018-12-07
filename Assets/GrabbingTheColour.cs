using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingTheColour : MonoBehaviour
{
    ColorPicker pickedColour;
    CharCustomSet saveLocation;
    // Use this for initialization
    void Start()
    {
        pickedColour = GameObject.Find("ColorPicker").GetComponent<ColorPicker>();
        saveLocation = GameObject.Find("Custom").GetComponent<CharCustomSet>();

        saveLocation.pickedColour = pickedColour;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
