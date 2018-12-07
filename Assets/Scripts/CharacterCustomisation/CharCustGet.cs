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

        filePath = Application.dataPath + "/SaveData/Data/" + fileName + ".xml";

        if (File.Exists(filePath))
        {
            Load();
        }


    }

    private void Start()
    {
        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charism" };

        meshFilter.mesh = meshes[id];
        //myColour = new Vector4(savedColour[0], savedColour[1], savedColour[2], savedColour[3]);
        //this.GetComponent<MeshRenderer>().material.color = myColour;
    }
    void Load()
    {
        var serializer = new XmlSerializer(typeof(CharacterData));
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            charData = serializer.Deserialize(stream) as CharacterData;
        }
        stats = charData.savedStats;
        statArray = charData.statArray;
        //myColour = charData.idColor;
        player.gameObject.name = charData.pName;

        myColour = new Vector4(charData.rgba[0], charData.rgba[1], charData.rgba[2], charData.rgba[3]);

        Debug.Log("I AM LOADING " + charData.idColor);
        player.GetComponent<Renderer>().material.color = myColour;

        id = charData.myMesh;
        charClass = (CharacterClass)System.Enum.Parse(typeof(CharacterClass), charData.sClass);
    }
}
