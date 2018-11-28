using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.IO;
using System;
using System.Xml.Serialization;

[Serializable]
public class SavedSettingsData
{
    public float volume;
    public int savedResolution;
    public bool fullScreen;
    public int qualityIndex;
    public QualitySettings graphics;
    public float graphicstest;
    public float brightnessSlider;
    
}
public class MenuUIHandler : MonoBehaviour
{
    #region Variables
    #region Audio Refs
    [Header("Audio")]
    public AudioSource musicAudio;//reference for main menu music audiosource
    public AudioSource menuAudio;//reference for interface sound effects audiosource
    public float volume;//references for volume float value
    public AudioMixer masterMixer, effectsMixer;//references for master audio mixer and interface effects audio mixer
    #endregion
    #region Visual Refs
    [Header("Visuals")]
    public Resolution[] resolutions;//reference for resolution index
    public Dropdown resolutionDropdown, graphicsDropdown;//references for the resolution and graphics dropdown UI elements
    public Slider brightnessSlider;//reference for the brightness slider UI element
    public GameObject mainMenu;//reference to the gameobject menu panel
    public GameObject optionsMenu;//reference to the gameobject menu panel
    public bool showOptions;//bool for displaying options menu
    public Image brightnessImage;//reference for the image ui element used to change brightness
    public Slider masterSlider, musicSlider, effectsAudioSlider;//references for the main music slider and interface effects slider UI elements
    public List<string> resolutionOptions = new List<string>();
    #endregion
    #region Save
    public static MenuUIHandler Instance = null;
    public string saveFilePath;
    public string fileName = "SettingsData";
    public SavedSettingsData data = new SavedSettingsData();
    #endregion
    #endregion

    private void OnDestroy()
    {
        Instance = null;
        Save();
    }
    public void Awake()
    {
        Instance = this;
        saveFilePath = Application.dataPath + "/SaveData/Data/" + fileName + ".xml";
        if (File.Exists(saveFilePath))
        {
            Load();
            brightnessSlider.value = 0.5f;

        }
        #region Resolutions
        //resolutions variable grabs the available resolutions unity supports
        resolutions = Screen.resolutions;
        //clears all the resolutions in the resloution drop down ui element
        resolutionDropdown.ClearOptions();
        //lists the available resolution options as a string



        int currentResolutionIndex = 0;
        //loop through each element in the resolutions array
        for (int i = 0; i < resolutions.Length; i++)
        {
            //displays a resolution option 
            string option = resolutions[i].width + "x" + resolutions[i].height;
            //adds the resolution option to our list
            resolutionOptions.Add(option);

            //if resolutions is equal to the monitors resolutions (by comparing the width and the height of our resolution)
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                //displays current resolution in index
                currentResolutionIndex = i;
            }
        }
        //adds the list with our resolution options to our dropdown
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        #endregion
        #region Sound
        musicAudio = GameObject.Find("MainMenuMusic").GetComponent<AudioSource>();
        musicSlider.value = -25f;
        musicSlider.value = musicAudio.volume;

        effectsAudioSlider.value = -25f;
        //musicSlider.value = PlayerPrefs.GetFloat("MainMixer", -25f);
        //effectsAudioSlider.value = PlayerPrefs.GetFloat("SoundEffect", -25f);
        #endregion

        #region Brightness

        #endregion
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Scene manager grabs the next active scene in the build index and loads it.
    }


    public void ExitGame()
    {
        Debug.Log("Quit it");
        Application.Quit();
        //Quits application
    }
    public void ToggleOptions()
    {
        //calls the bool optiontoggle function
        OptionToggle();

    }
    bool OptionToggle()
    {
        if (showOptions)//showOptions == true or showOptions is true
        {
            showOptions = false;
            //Set to not display Options Menu as it is not actived
            mainMenu.SetActive(true);
            //Show Main Menu as Options is not being viewed
            optionsMenu.SetActive(false);
            //returns the data as true
            return true;

        }
        else
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            masterSlider = GameObject.FindGameObjectWithTag("MasterSlider").GetComponent<Slider>();
            effectsAudioSlider = GameObject.FindGameObjectWithTag("InterfaceEffectsSlider").GetComponent<Slider>();
            resolutionDropdown = GameObject.Find("ResolutionDropDown").GetComponent<Dropdown>();
            graphicsDropdown = GameObject.Find("QualityDropDown").GetComponent<Dropdown>();
            //volumeSlider.value = mainAudio.volume;

            brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
            var tempColor = brightnessImage.color;
            brightnessSlider.value = 1.0f - tempColor.a;
            return false;


        }
    }
    public void MasterVolume(float volume)
    {
        masterMixer.SetFloat("Volume", volume);

        //musicSlider.value = musicAudio.volume;
        //audioMixer = musicSlider.value;
        //musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();

        //musicSlider.value = musicAudio.volume;
        //musicAudio.volume = musicSlider.value;

        //gameAudioSlider = GameObject.Find("Game Audio Slider").GetComponent<Slider>();

        //menuAudioSlider = GameObject.Find("Sound Effects Slider").GetComponent<Slider>();
        //menuAudio.volume = menuAudioSlider.value;

    }

    //updates value of the sliders
    public void UpdateSliders()
    {
        //PlayerPrefs obtains the value of the master mixer "Volume" and is named as musicValue variable
        float musicValue = PlayerPrefs.GetFloat("Volume");
        //value of the slider that controls music is equal to the volume
        musicSlider.value = musicValue;
    }
    //changes the volume of the interface effects
    public void EffectsVolume(float volume)
    {

        effectsMixer.SetFloat("SoundEffects", volume);
    }
    //Visual Quality function
    public void SetQuality(int qualityIndex)
    {
        //Sets the visual Quality by obtaining the Quality settings set in Unity project settings as a index.
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void ChangeBrightness()
    {
        //temporary color is equal to the brightnessImage's color
        var tempColor = brightnessImage.color;
        tempColor.a = 0.5f - brightnessSlider.value;
        brightnessImage.color = tempColor;

        Debug.Log("Bright");
    }

    //Saves the changed values/data to PlayerPrefs and sets it 
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MainMixer", musicSlider.value);
        PlayerPrefs.SetFloat("SoundEffect", effectsAudioSlider.value);

    }
    //Sets the resloution in the resolution index (int value)
    public void SetResolution(int resolutionIndex)
    {
        //
        Resolution resolution = resolutions[resolutionIndex];
        //Sets the selected resolution in the resolution index by its width and height. Also sets it as fullscreen.
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Toggles between windowed and fullscreen
    public void ToggleFullScreen(bool isFullscreen)
    {
        //toggles fullscreen mode via bool in event system.
        Screen.fullScreen = isFullscreen;

    }
    public void Save()
    {
        var serializer = new XmlSerializer(typeof(SavedSettingsData));
        using (var stream = new FileStream(saveFilePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
        data.fullScreen = Screen.fullScreen;

        data.savedResolution = resolutionDropdown.value;
        data.brightnessSlider = brightnessSlider.value;

    }
    public void Load()
    {
        var serializer = new XmlSerializer(typeof(SavedSettingsData));
        using (var stream = new FileStream(saveFilePath, FileMode.Open))
        {
            data = serializer.Deserialize(stream) as SavedSettingsData;


        }
        Screen.fullScreen = data.fullScreen;
        brightnessSlider.value = data.brightnessSlider;
        resolutionDropdown.value = data.savedResolution;
    }
}
