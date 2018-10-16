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
    void Start ()
    {
        keys.Add("Forward", KeyCode.W);
        keys.Add("Back", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("Sprint", KeyCode.LeftShift);
        keys.Add("Fire", KeyCode.Mouse0);
        keys.Add("Test", KeyCode.J);

        forwardText.text = keys["Forward"].ToString();
        backText.text = keys["Back"].ToString();
        leftText.text = keys["Left"].ToString();
        rightText.text = keys["Right"].ToString();
        jumpText.text = keys["Jump"].ToString();
        sprintText.text = keys["Sprint"].ToString();
        fireText.text = keys["Fire"].ToString();
        testText.text = keys["Test"].ToString();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(keys["Forward"]))
        {
            Debug.Log("Forward");
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
}
