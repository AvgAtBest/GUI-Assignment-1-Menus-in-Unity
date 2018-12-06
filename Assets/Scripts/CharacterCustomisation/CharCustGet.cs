using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class CharCustGet : MonoBehaviour
{
    public Mesh[] meshes; 
    public MeshFilter meshFilter;
    public int id;
    public float[] savedColour = new float[4];
    public Color myColour;

    void Start ()
    {
        meshFilter.mesh = meshes[id];
        myColour = new Vector4(savedColour[0], savedColour[1], savedColour[2], savedColour[3]);
        this.GetComponent<MeshRenderer>().material.color = myColour;
    }
	
    void Load()
    {

    }
}
