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
    public CharCustomSet customSet;
    public string fileName = "PlayerData";
    public string filePath;
    public CharacterData charData = new CharacterData();
    public CharacterClass charClass;
    public string[] statArray = new string[6]; //names of the available stats in the array
    public int[] stats = new int[6];//the stat points associated with stat array
    public static CharCustGet Instance = null;

    public GameObject player;

    private void OnDestroy()
    {
        Instance = null;
        //destroys the old saved data
    }

    void Awake()
    {
        
        player = GameObject.Find("Player");
        //load here
        Instance = this;
        //The File path where the file will be loaded from
        filePath = Application.dataPath + "/SaveData/Data/" + fileName + ".xml";
        //if the filepath and file exists
        if (File.Exists(filePath))
        {
            //calls load function
            Load();
        }


    }

    private void Start()
    {
        //the array of the stat names
        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charism" };
        //the players mesh and mesh filter via the id index
        meshFilter.mesh = meshes[id];

    }
    void Load()
    {
        //Loads the stored data in CharacterData
        var serializer = new XmlSerializer(typeof(CharacterData));
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            charData = serializer.Deserialize(stream) as CharacterData;
        }
        //loads the stats from the charData savedStats variable
        stats = charData.savedStats;
        //loads the stat name from the charData stat arrays
        statArray = charData.statArray;
        //the players name is the name from charData pName variable
        player.gameObject.name = charData.pName;
        //color via the rgba variables
        myColour = new Vector4(charData.rgba[0], charData.rgba[1], charData.rgba[2], charData.rgba[3]);

        Debug.Log("I AM LOADING " + charData.idColor);

        //gets the renderer component and its material and color and changes it to mycolor
        player.GetComponent<Renderer>().material.color = myColour;
        //loads the charData mesh from the id
        id = charData.myMesh;
        //loads the character class name and converts enum to string
        charClass = (CharacterClass)System.Enum.Parse(typeof(CharacterClass), charData.sClass);
    }
}
