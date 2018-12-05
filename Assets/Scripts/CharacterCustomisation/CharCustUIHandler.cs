using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCustUIHandler : MonoBehaviour
{
    public bool showCharacterSelection;
    public bool showClassSelection;
    public bool showCustomSelection;
    public bool showFinish;
    public Button charSelect, classSelect, customSelect, finishSelect;
    public GameObject charPanel, classPanel, customPanel, finishPanel;

    // Use this for initialization
    void Start()
    {


    }

    //toggles character select

    //bool CharSelectToggle()
    //{
    //    if (showCharacterSelection)
    //    {
    //        showCharacterSelection = true;
    //        charPanel.SetActive(true);
    //        classPanel.SetActive(false);
    //        customPanel.SetActive(false);
    //        finishPanel.SetActive(false);
    //        return true;

    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    bool CharSelectToggle()
    {


        showCharacterSelection = true;
        showClassSelection = false;
        showCustomSelection = false;
        showFinish = false;

        charPanel.SetActive(true);
        classPanel.SetActive(false);
        customPanel.SetActive(false);
        finishPanel.SetActive(false);
        return true;



    }
    bool ClassSelectToggle()
    {

        showClassSelection = true;
        showCharacterSelection = false;
        showCustomSelection = false;
        showFinish = false;

        charPanel.SetActive(false);
        classPanel.SetActive(true);
        customPanel.SetActive(false);
        finishPanel.SetActive(false);
        return true;
    }
    bool CustomSelectToggle()
    {
        showCustomSelection = true;
        showClassSelection = false;
        showCharacterSelection = false;
        showFinish = false;

        charPanel.SetActive(false);
        classPanel.SetActive(false);
        customPanel.SetActive(true);
        finishPanel.SetActive(false);
        return true;
    }
    bool FinishToggle()
    {
        showCustomSelection = false;
        showClassSelection = false;
        showCharacterSelection = false;
        showFinish = true;

        charPanel.SetActive(false);
        classPanel.SetActive(false);
        customPanel.SetActive(false);
        finishPanel.SetActive(true);
        return true;
    }
    private void OnGUI()
    {

    }
}
