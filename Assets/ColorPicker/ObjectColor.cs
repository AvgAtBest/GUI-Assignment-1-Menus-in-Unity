using UnityEngine;
using System.Collections;

public class ObjectColor : MonoBehaviour
{
    CharCustomSet saveLocation;

    private void Awake()
    {
        saveLocation = GameObject.Find("Custom").GetComponent<CharCustomSet>();
    }

    public float[] rgba = new float[4];
	void OnSetColor(Color color)
	{
		Material mt = new Material(GetComponent<Renderer>().sharedMaterial);
		mt.color = color;
		GetComponent<Renderer>().material = mt;
        rgba[0] = mt.color.r;
        rgba[1] = mt.color.g;
        rgba[2] = mt.color.b;
        rgba[3] = mt.color.a;
        Debug.Log(mt.color.ToString());

    }

    void OnGetColor(ColorPicker picker)
	{
		picker.NotifyColor(GetComponent<Renderer>().material.color);
        Debug.Log(GetComponent<Renderer>().material.color.ToString());
        saveLocation.characterColour = GetComponent<Renderer>().material.color;

    }
}
