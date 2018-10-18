using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KeyBindings : MonoBehaviour
{

    public TextMeshProUGUI forwardText;
    public TextMeshProUGUI backText, leftText, rightText, sprintText, jumpText, fireText, testText;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private GameObject currentKey;
    // Use this for initialization
    void Start()
    {
        //string forward = PlayerPrefs.GetString("Forward");
        keys.Add("Forward",(KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")));
        keys.Add("Back", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Back", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        keys.Add("Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", "Mouse0")));
        keys.Add("Test", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Test", "J")));

        forwardText.text = keys["Forward"].ToString();
        backText.text = keys["Back"].ToString();
        leftText.text = keys["Left"].ToString();
        rightText.text = keys["Right"].ToString();
        jumpText.text = keys["Jump"].ToString();
        sprintText.text = keys["Sprint"].ToString();
        fireText.text = keys["Fire"].ToString();
        testText.text = keys["Test"].ToString();
    }

    //void OnApplicationPause(bool pauseStatus)
    //{
    //    if (pauseStatus)
    //        SaveKeys();
    //}


    void Update()
    {
        if (Input.GetKeyDown(keys["Forward"]))
        {
            Debug.Log("Forward");
        }
        if (Input.GetKeyDown(keys["Back"]))
        {
            Debug.Log("Back");
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["Jump"]))
        {
            Debug.Log("Jump");
        }
        if (Input.GetKeyDown(keys["Sprint"]))
        {
            Debug.Log("Sprint");
        }
        if (Input.GetKeyDown(keys["Fire"]))
        {
            Debug.Log("Fire");
        }
        if (Input.GetKeyDown(keys["Test"]))
        {
            Debug.Log("Test");
        }
    }
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
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
    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
        Debug.Log("Save");
    }
}
