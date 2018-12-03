using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Xml.Serialization;

[Serializable]
//Class used to save the keyData
public class KeyData
{
    //keycodes listed as forward, back, left, right etc...
    public KeyCode forward, back, left, right, jump, sprint, fire, test;
}
public class KeyBindings : MonoBehaviour
{
    //Textmesh pro UI elements 
    public TextMeshProUGUI forwardText;
    public TextMeshProUGUI backText, leftText, rightText, sprintText, jumpText, fireText, testText;

    //the clicked current key
    private GameObject currentKey;
    //the data used to contain the saved variables
    private KeyData data = new KeyData();

    //string for the saveFilePath variable
    public string saveFilePath;
    //string of the file name
    public string fileName = "GameData";
    //the Keybindings script is set to static to allow access from the KeyData CLass
    public static KeyBindings Instance = null;
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    // Use this for initialization
    private void Awake()
    {
        //create this script
        Instance = this;
        //save file path is set to /SaveData/Data + the file's name + the xml file directory
        saveFilePath = Application.dataPath + "/SaveData/Data/" + fileName + ".xml";
        //if the file exists in the saveFilePath
        if (File.Exists(saveFilePath))
        {

            Load();//loads the data
            //Loads the text into the keys under their respective buttons (converts it to string)
            forwardText.text = keys["ForwardButton"].ToString();
            backText.text = keys["BackButton"].ToString();
            leftText.text = keys["LeftButton"].ToString();
            rightText.text = keys["RightButton"].ToString();
            jumpText.text = keys["JumpButton"].ToString();
            sprintText.text = keys["SprintButton"].ToString();
            fireText.text = keys["FireButton"].ToString();
            testText.text = keys["TestButton"].ToString();
        }
    }
    private void OnDestroy()
    {

        Instance = null;
        //Saves the data
        Save();
    }


    void OnGUI()
    {
        //if the current key isn't null
        if (currentKey != null)
        {
            //allows you to change the text in the button
            Event e = Event.current;
            //if the event is reading a keyboard input
            if (e.isKey)
            {
                Debug.Log(currentKey.name);
                //changes the key to the key input into the keyboard and converts the keycode to string
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                //sets the current key as now null
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        //when the gameobject is clicked, the current key that is clicked allows for input of new keys
        currentKey = clicked;

    }

    public void Save()
    {
        //new xmlSerialiser containing the data types in KeyData, and creates a save file in the saveFilePath
        var serializer = new XmlSerializer(typeof(KeyData));
        using (var stream = new FileStream(saveFilePath, FileMode.Create))
        {
            //serializes the data in stream
            serializer.Serialize(stream, data);
        }
        //grabs the keycodes name in KeyData and sets them as the keys in each button
        data.forward = keys["ForwardButton"];
        data.back = keys["BackButton"];
        data.left = keys["LeftButton"];
        data.right = keys["RightButton"];
        data.sprint = keys["SprintButton"];
        data.jump = keys["JumpButton"];
        data.fire = keys["FireButton"];
        data.test = keys["TestButton"];


    }
    public void Load()
    {
        //loads the xml file
        var serializer = new XmlSerializer(typeof(KeyData));
        using (var stream = new FileStream(saveFilePath, FileMode.Open))
        {
            data = serializer.Deserialize(stream) as KeyData;

            
        }
        //grabs the keys in each keybinding button and sets them as the keycode in KeyData
        keys["ForwardButton"] = data.forward;
        keys["BackButton"] = data.back;
        keys["LeftButton"] = data.left;
        keys["RightButton"] = data.right;
        keys["SprintButton"] = data.sprint;
        keys["JumpButton"] = data.jump;
        keys["FireButton"] = data.fire;
        keys["TestButton"] = data.test;
        
    }
}
