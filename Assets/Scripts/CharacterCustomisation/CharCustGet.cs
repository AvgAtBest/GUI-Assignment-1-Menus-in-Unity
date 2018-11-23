using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCustGet : MonoBehaviour
{
    public CharCustomSet charCustH;
    public Mesh playerModel;


	// Use this for initialization
	void Start ()
    {
        playerModel = GameObject.Find("CharModel").GetComponent<Mesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
