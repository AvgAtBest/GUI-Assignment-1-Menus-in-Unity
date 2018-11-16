using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Xml.Serialization;

[Serializable]
public class KeyData
{
    public KeyCode forward, back, left, right, jump, sprint, fire, test;
}
public class KeyBindings : MonoBehaviour
{

    public TextMeshProUGUI forwardText;
    public TextMeshProUGUI backText, leftText, rightText, sprintText, jumpText, fireText, testText;

    //private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private GameObject currentKey;
    private KeyData data = new KeyData();

    public string saveFilePath;
    public string fileName = "GameData";
    public static KeyBindings Instance = null;
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    // Use this for initialization
    private void Awake()
    {
        Instance = this;

        saveFilePath = Application.dataPath + "/SaveData/Data/" + fileName + ".xml";
        if (File.Exists(saveFilePath))
        {

            Load();
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
        Save();
    }
    void Start()
    {
        //string forward = PlayerPrefs.GetString("Forward");
        //data.keys.Add("Forward",(KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")));
        //data.keys.Add("Back", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Back", "S")));
        //data.keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        //data.keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        //data.keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        //data.keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        //data.keys.Add("Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", "Mouse0")));
        //data.keys.Add("Test", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Test", "J")));

        //forwardText.text = data.keys["Forward"].ToString();
        //backText.text = data.keys["Back"].ToString();
        //leftText.text = data.keys["Left"].ToString();
        //rightText.text = data.keys["Right"].ToString();
        //jumpText.text = data.keys["Jump"].ToString();
        //sprintText.text = data.keys["Sprint"].ToString();
        //fireText.text = data.keys["Fire"].ToString();
        //testText.text = data.keys["Test"].ToString();
    }

    //void OnApplicationPause(bool pauseStatus)
    //{
    //    if (pauseStatus)
    //        SaveKeys();
    //}


    void Update()
    {
        //if (Input.GetKeyDown(keys["Forward"]))
        //{
        //    Debug.Log("Forward");
        //}
        //if (Input.GetKeyDown(keys["Back"]))
        //{
        //    Debug.Log("Back");
        //}
        //if (Input.GetKeyDown(keys["Left"]))
        //{
        //    Debug.Log("Left");
        //}
        //if (Input.GetKeyDown(keys["Right"]))
        //{
        //    Debug.Log("Right");
        //}
        //if (Input.GetKeyDown(keys["Jump"]))
        //{
        //    Debug.Log("Jump");
        //}
        //if (Input.GetKeyDown(keys["Sprint"]))
        //{
        //    Debug.Log("Sprint");
        //}
        //if (Input.GetKeyDown(keys["Fire"]))
        //{
        //    Debug.Log("Fire");
        //}
        //if (Input.GetKeyDown(keys["Test"]))
        //{
        //    Debug.Log("Test");
        //}
    }
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                Debug.Log(currentKey.name);
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;

    }
    //public void SaveKeys()
    //{
    //    foreach (var key in keys)
    //    {
    //        PlayerPrefs.SetString(key.Key, key.Value.ToString());
    //    }
    //    PlayerPrefs.Save();
    //    Debug.Log("Save");
    //}
    public void Save()
    {
        var serializer = new XmlSerializer(typeof(KeyData));
        using (var stream = new FileStream(saveFilePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
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
        var serializer = new XmlSerializer(typeof(KeyData));
        using (var stream = new FileStream(saveFilePath, FileMode.Open))
        {
            data = serializer.Deserialize(stream) as KeyData;

            
        }
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
