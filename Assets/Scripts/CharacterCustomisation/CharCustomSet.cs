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
    //Variables
    public int pModelIndex;//Model index
    public int charClassIndex;//Character Class index
    public string pName;//Players name
    public string sClass;//Character class name
    public Color idColor;//selected color
    public float[] rgba = new float[4];//array float for rgba of colors
    public Mesh[] mymesheses;//mesh
    public int myMesh;//selected mesh
    public int[] savedStats;//the saved stats
    public string[] statArray;//the stat array
    
}

public class CharCustomSet : MonoBehaviour
{
    #region CharModels
    //Variables for the Character models index, mesh and mesh filter
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
    //Character name, input ui and the gameobject of the color
    public string charName = "Name";//player name
    public GameObject nameInputUI;
    public GameObject player;
    public Color characterColour;

    #endregion
    #region Toggling UI
    //UI Elements used in each of the 4 panels (Character, Class, Color Customiastion and Finish)
    public bool charS1;
    public bool class1;
    public bool cust1;
    public bool fin1;
    #endregion
    #region Save
    //The file name, filepath and Class used for the saving of the character data
    public string fileName = "PlayerData";
    public string filePath;
    public CharacterData charData = new CharacterData();

    #endregion
    //ColorPicker Script
    public ColorPicker pickedColour;

    void Awake()
    {
        //Turns on character selection 1
        charS1 = true;
        meshFilt = GameObject.FindGameObjectWithTag("Player").GetComponent<MeshFilter>();
        //the names of the stat names,class types and selected models in their respective indexes
        statArray = new string[] { "Strength", "Agility", "Constitution", "Elemental", "Charisma", "Intelligence" };
        selectedClass = new string[] { "Warrior", "Mage", "Tank", "Hunter", "Healer", "Thief" };
        selectedModel = new string[] { "Capsule", "Cylinder" };
        //called the available classes function
        AvailableClasses(selectedIndex);
        //Calls the set model function
        SetModel("Model", modelIndex);
        //The File path where the file will be saved
        filePath = Application.dataPath + "/SaveData/Data/" + fileName + ".xml";

    }

    private void Update()
    {
        //The players desired name in the input field
        charName = nameInputUI.GetComponent<Text>().text;
    }


    void SetModel(string type, int next)
    {
        //index variables for the amount of meshes that can be changed
        int index = 0, max = 0;
        Mesh[] pMesh = new Mesh[0];

        //switches between the model types in the mesh array
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
        //Loads the gamescene and saves the selected customisation options 
        SceneManager.LoadScene(2);
        Save();
    }
    //toggles the ONGUI elements for the Character Selection Panel, and toggles off all aother ONGUI elements not present on this selection
    public void ToggleCharPanel()
    {
        
        charS1 = true;
        class1 = false;
        cust1 = false;
        fin1 = false;
    }
    //toggles the ONGUI elements for the Character Class Panel, and toggles off all aother ONGUI elements not present on this selection
    public void ToggleClassPanel()
    {
        charS1 = false;
        class1 = true;
        cust1 = false;
        fin1 = false;
    }
    //toggles the ONGUI elements for the Character Customisation Panel, and toggles off all aother ONGUI elements not present on this selection
    public void ToggleCustPanel()
    {
        charS1 = false;
        class1 = false;
        cust1 = true;
        fin1 = false;
    }
    //toggles the ONGUI elements for the Finish Panel, and toggles off all aother ONGUI elements not present on this selection
    public void ToggleFinPan()
    {
        charS1 = false;
        class1 = false;
        cust1 = false;
        fin1 = true;
    }
    private void OnGUI()
    {
        //width and height of the screen
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        int i = 0;

        //if the character UI panel is active
        if (charS1 == true)
        {
            //Creates a UI Box with the model names in the model array. 
            GUI.Box(new Rect(1.35f * scrW, 5.5f * scrH, 1f * scrW, 0.5f * scrH), selectedModel[modelIndex]);

            if (GUI.Button(new Rect(2.35f * scrW, 5.5f * scrH, 0.5f * scrW, 0.5f * scrH), ">"))
            {
                //selects the next option in the SetModel Array
                SetModel("Model", 1);

            }
            if (GUI.Button(new Rect(0.85f * scrW, 5.5f * scrH, 0.5f * scrW, 0.5f * scrH), "<"))
            {


                //Goes back to the previous model in the array
                SetModel("Model", -1);
            }
        }

        //if the character class panel is active
        if (class1)
        {
            #region CharClass
            i = 0;
            //Creates a UI Box with the choosable Stat names  in the stat array. 
            GUI.Box(new Rect(0.95f * scrW, 2f * scrH + i * (0.75f * scrH), 1.85f * scrW, 0.5f * scrH), "Class");
            i++;
            GUI.Box(new Rect(1.35f * scrW, 2.25f * scrH + i * (0.75f * scrH), 1f * scrW, 0.5f * scrH), selectedClass[selectedIndex]);

            //selects the next class name in the stat array
            if (GUI.Button(new Rect(2.35f * scrW, 2.25f * scrH + i * (0.75f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
            {
                selectedIndex++;
                if (selectedIndex > selectedClass.Length - 1)
                {
                    selectedIndex = 0;
                }
                AvailableClasses(selectedIndex);

            }
            //Goes back to the previous class name in the array
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
            //UI Box displaying the spendable points
            GUI.Box(new Rect(1.35f * scrW, 3.5f * scrH, 1f * scrW, 0.5f * scrH), "P:" + statpoints);
            for (int s = 0; s < 6; s++)
            {
                //If there are stat points to spend
                if (statpoints > 0)
                {
                    //If the + button is clicked
                    if (GUI.Button(new Rect(2.65f * scrW, 4f * scrH + s * (0.5f * scrH), 0.75f * scrW, 0.5f * scrH), "+ "))

                    {
                        //Loses one stat points
                        statpoints--;
                        //Adds one stat point to the chosen temp stat via the index in the for loop
                        tempStats[s]++;
                    }
                }
                //left hand side width   //height down                       //right width   //hight up
                //Creates a UI element with the stat name in the stat array + the stat points and temp stats
                GUI.Box(new Rect(1f * scrW, 4f * scrH + s * (0.5f * scrH), 1.65f * scrW, 0.5f * scrH), statArray[s] + ": " + (stats[s] + tempStats[s]));
                //if their are less that 10 stat points and their are more then 0 points in any of the temp stats
                if (statpoints < 10 && tempStats[s] > 0)
                {
                    //If the - button is pressed
                    if (GUI.Button(new Rect(0.25f * scrW, 4f * scrH + s * (0.5f * scrH), 0.75f * scrW, 0.5f * scrH), "- "))
                    {
                        //Adds one point to the stat points, and loses one from the temp stat via the index in the for loop
                        statpoints++;
                        tempStats[s]--;
                    }

                }
            }
            #endregion

        }




    }

  
    //The list of the available classes, with their default temp stat values for each respective class
    void AvailableClasses(int className)
    {
        switch (className)
        {
            //case 0 (default) is warrior, with the it's default temp stats. Case 1 is Mage with it's default temp stats etc..
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


        //name
        //4 floats of colour
        //mesh index

        //the chardata variable in charData is equal to the charName 
        charData.pName = charName;


        //The character model's selected color is being saved to the pickedColor float index variable in charData 
        float[] fookColour = new float[4];
        fookColour[0] = pickedColour.SelectedColor.r;
        fookColour[1] = pickedColour.SelectedColor.g;
        fookColour[2] = pickedColour.SelectedColor.b;
        fookColour[3] = pickedColour.SelectedColor.a;
        charData.rgba = fookColour;
        
        Debug.Log("I AM SAVING " + charData.rgba);
        //saving the selected mesh to charData
        charData.myMesh = modelIndex;
        //saves the selected class name as a string to charData
        charData.sClass = selectedClass[selectedIndex].ToString();
        //saves the stats to charData
        charData.savedStats = stats;
        //saves the stat name to the charData
        charData.statArray = statArray;
        //new xml serializer containing the data types in CharacterData
        var serializer = new XmlSerializer(typeof(CharacterData));
        using (var stream = new FileStream(filePath, FileMode.Create))//creates a file in the saved File Path
        {
            serializer.Serialize(stream, charData);
        }
    }
}
//The Character Classes
public enum CharacterClass
{
    Warrior,
    Mage,
    Tank,
    Hunter,
    Healer,
    Thief

}