using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.EventSystems;


[Serializable]
public class CharacterData
{
    public int pModelIndex;
    public int charClassIndex;
    public string pName;
    public string sClass;
    public int modelIndexffs;
    public Color idColor;
    public float[] rgba = new float[4];
    public Mesh[] mymesheses;
    public int myMesh;
    public int[] savedStats;
    public string[] statArray;
    
}

public class CharCustomSet : MonoBehaviour
{
    #region CharRenderer (TESTING)

    //public Color[] color = new Color[10];
    //public int colorMax;
    public int modelMax;
    public MeshFilter meshFilt;
    public Mesh[] mesh;
    public int modelIndex;
    #endregion
    #region CharClass
    public string[] statArray = new string[6]; //names of the available stats in the array
    public int[] stats = new int[6];//the stat points associated with stat array
    public int[] tempStats = new int[6];//temporary stats (used when new character is made)
    public int statpoints = 10;//available stats points that can be spent
    public CharacterClass charClass = CharacterClass.Warrior;//Available classes to be chosen (default set to warrior)
    public string[] selectedClass = new string[6];
    public string[] selectedModel = new string[2];
    public int selectedIndex = 0;
    public int mIndex = 0;
    #endregion
    #region Character
    public string charName = "Name";//player name
    public GameObject nameInputUI;
    public GameObject player;
    public Color characterColour;

    #endregion
    #region Toggling UI
    public bool charS1;
    public bool class1;
    public bool cust1;
    public bool fin1;
    #endregion
    #region Save
    public string fileName = "PlayerData";
    public string filePath;
    public CharacterData charData = new CharacterData();

    #endregion

    public ColorPicker pickedColour;

    void Awake()
    {
        charS1 = true;
        meshFilt = GameObject.FindGameObjectWithTag("Player").GetComponent<MeshFilter>();
        statArray = new string[] { "Strength", "Agility", "Constitution", "Elemental", "Charisma", "Intelligence" };
        selectedClass = new string[] { "Warrior", "Mage", "Tank", "Hunter", "Healer", "Thief" };
        selectedModel = new string[] { "Capsule", "Cylinder" };
        AvailableClasses(selectedIndex);
        SetModel("Model", modelIndex);

        filePath = Application.dataPath + "/SaveData/Data/" + fileName + ".xml";

    }

    private void Update()
    {
        charName = nameInputUI.GetComponent<Text>().text;
    }


    void SetModel(string type, int next)
    {
        // int i = 0, next = 0,
        int index = 0, max = 0;
        Mesh[] pMesh = new Mesh[0];

        switch (type)
        {
            case "Model":
                index = modelIndex;
                max = modelMax;

                break;
        }
        index += next;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        meshFilt.mesh = mesh[index];

        switch (type)
        {
            case "Model":
                modelIndex = index;
                break;

        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(2);
        Save();
    }
    public void ToggleCharPanel()
    {
        charS1 = true;
        class1 = false;
        cust1 = false;
        fin1 = false;
    }
    public void ToggleClassPanel()
    {
        charS1 = false;
        class1 = true;
        cust1 = false;
        fin1 = false;
    }
    public void ToggleCustPanel()
    {
        charS1 = false;
        class1 = false;
        cust1 = true;
        fin1 = false;
    }
    public void ToggleFinPan()
    {
        charS1 = false;
        class1 = false;
        cust1 = false;
        fin1 = true;
    }
    private void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        int i = 0;


        if (charS1 == true)
        {
            GUI.Box(new Rect(1.35f * scrW, 5.5f * scrH, 1f * scrW, 0.5f * scrH), selectedModel[modelIndex]);

            if (GUI.Button(new Rect(2.35f * scrW, 5.5f * scrH, 0.5f * scrW, 0.5f * scrH), ">"))
            {
               
                SetModel("Model", 1);

            }
            if (GUI.Button(new Rect(0.85f * scrW, 5.5f * scrH, 0.5f * scrW, 0.5f * scrH), "<"))
            {
                //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1

              
                SetModel("Model", -1);
            }
        }


        if (class1)
        {
            #region CharClass
            i = 0;
            GUI.Box(new Rect(0.95f * scrW, 2f * scrH + i * (0.75f * scrH), 1.85f * scrW, 0.5f * scrH), "Class");
            i++;
            GUI.Box(new Rect(1.35f * scrW, 2.25f * scrH + i * (0.75f * scrH), 1f * scrW, 0.5f * scrH), selectedClass[selectedIndex]);


            if (GUI.Button(new Rect(2.35f * scrW, 2.25f * scrH + i * (0.75f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
            {
                selectedIndex++;
                if (selectedIndex > selectedClass.Length - 1)
                {
                    selectedIndex = 0;
                }
                AvailableClasses(selectedIndex);

            }
            if (GUI.Button(new Rect(0.85f * scrW, 2.25f * scrH + i * (0.75f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = selectedClass.Length - 1;
                }
                AvailableClasses(selectedIndex);
            }
            #endregion
            #region Stats
            GUI.Box(new Rect(1.35f * scrW, 3.5f * scrH, 1f * scrW, 0.5f * scrH), "P:" + statpoints);
            for (int s = 0; s < 6; s++)
            {
                if (statpoints > 0)
                {
                    if (GUI.Button(new Rect(2.65f * scrW, 4f * scrH + s * (0.5f * scrH), 0.75f * scrW, 0.5f * scrH), "+ "))

                    {
                        statpoints--;
                        tempStats[s]++;
                    }
                }
                //left hand side width   //height down                       //right width   //hight up
                GUI.Box(new Rect(1f * scrW, 4f * scrH + s * (0.5f * scrH), 1.65f * scrW, 0.5f * scrH), statArray[s] + ": " + (stats[s] + tempStats[s]));
                if (statpoints < 10 && tempStats[s] > 0)
                {
                    if (GUI.Button(new Rect(0.25f * scrW, 4f * scrH + s * (0.5f * scrH), 0.75f * scrW, 0.5f * scrH), "- "))
                    {
                        statpoints++;
                        tempStats[s]--;
                    }

                }
            }
            #endregion

        }

        //if (GUI.Button(new Rect(0.85f * scrW, 5.5f * scrH, 0.5f * scrW, 0.5f * scrH), "<"))
        //{
        //    //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
        //    SetModel("Model", -1);
        //}
        //GUI.Box(new Rect(1.35f * scrW, 5.5f * scrH, 1f * scrW, 0.5f * scrH), "Capsule");
        ////GUI button on the left of the screen with the contence >
        //if (GUI.Button(new Rect(2.35f * scrW, 5.5f * scrH, 0.5f * scrW, 0.5f * scrH), ">"))
        //{
        //    SetModel("Model", 1);
        //    //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        //}


    }

  

    void AvailableClasses(int className)
    {
        switch (className)
        {
            case 0:
                stats[0] = 6;
                stats[1] = 6;
                stats[2] = 6;
                stats[3] = 5;
                stats[4] = 5;
                stats[5] = 5;
                charClass = CharacterClass.Warrior;
                break;
            case 1:
                stats[0] = 4;
                stats[1] = 6;
                stats[2] = 5;
                stats[3] = 10;
                stats[4] = 5;
                stats[5] = 7;
                charClass = CharacterClass.Mage;
                break;
            case 2:
                stats[0] = 9;
                stats[1] = 2;
                stats[2] = 10;
                stats[3] = 1;
                stats[4] = 4;
                stats[5] = 4;
                charClass = CharacterClass.Tank;
                break;
            case 3:
                stats[0] = 4;
                stats[1] = 9;
                stats[2] = 6;
                stats[3] = 2;
                stats[4] = 5;
                stats[5] = 8;
                charClass = CharacterClass.Hunter;
                break;
            case 4:
                stats[0] = 3;
                stats[1] = 6;
                stats[2] = 7;
                stats[3] = 8;
                stats[4] = 5;
                stats[5] = 6;
                charClass = CharacterClass.Healer;
                break;
            case 5:
                stats[0] = 4;
                stats[1] = 8;
                stats[2] = 5;
                stats[3] = 4;
                stats[4] = 9;
                stats[5] = 7;
                charClass = CharacterClass.Thief;
                break;

        }


    }
    public void Save()
    {
        //charData.meshIndex[modelIndex] = modelIndex;
        //charData.pName = charName;

        //name
        //4 floats of colour
        //mesh index

        charData.pName = charName;

        //charData.idColor = characterColour;

        float[] fookColour = new float[4];
        fookColour[0] = pickedColour.SelectedColor.r;
        fookColour[1] = pickedColour.SelectedColor.g;
        fookColour[2] = pickedColour.SelectedColor.b;
        fookColour[3] = pickedColour.SelectedColor.a;

        charData.rgba = fookColour;
        
        Debug.Log("I AM SAVING " + charData.rgba);
        charData.myMesh = modelIndex;
        charData.sClass = selectedClass[selectedIndex].ToString();

        charData.savedStats = stats;
        charData.statArray = statArray;
        var serializer = new XmlSerializer(typeof(CharacterData));
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, charData);
        }
    }
}
public enum CharacterClass
{
    Warrior,
    Mage,
    Tank,
    Hunter,
    Healer,
    Thief

}