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

[Serializable]//Allows for data to be serialized
//Class used to save settings data from MenuUIHandler
public class SavedSettingsData
{
    //xml references for saving and loading data to respective areas
    public float savedMusicVolume;//the saved music volume value
    public float savedInterfaceVolume;//thesaved interface volume value
    public int savedResolution; //the saved resolution 
    public bool fullscreenPref;//the save fullscreen preference
    public int qualityIndex;//the visual quality chosen in the index
    public float brightnessSlider;//the saved brightness slider value
    
}
//Class containing the functions for the ui elements of the main menu
public class MenuUIHandler : MonoBehaviour
{
    #region Variables
    #region Audio Refs
    [Header("Audio")]
    public AudioSource musicAudio;//reference for main menu music audiosource
    public AudioSource interfaceAudio;//reference for interface sound effects audiosource
    public AudioMixer musicAudioMixer, interfaceAudioMixer;//references for music audio mixer and interface effects audio mixer
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
    public Slider musicSlider, interfaceAudioSlider;//references for the main music slider and interface effects slider UI elements
    public Toggle uiToggleFull;//UI toggle for fullscreen toggle
    #endregion
    #region Save
    //the menuhandler script is set to static to allow access from the SaveSettingsData CLass
    public static MenuUIHandler Instance = null;
    //the string of  the saveFilePath
    public string saveFilePath;
    //the string of the file name
    public string fileName = "SettingsData";
    //the data used to contain the saved variables
    public SavedSettingsData data = new SavedSettingsData();
    #endregion
    #endregion

    private void OnDestroy()
    {
        Instance = null;
        //destroys the old saved data
    }
    public void Awake()
    {
        Instance = this;
        
        #region Resolutions
        //resolutions variable grabs the available resolutions unity supports
        resolutions = Screen.resolutions;
        //clears all the resolutions in the resloution drop down ui element
        resolutionDropdown.ClearOptions();
        //lists the available resolution options as a string
        List<string> options = new List<string>();


        int currentResolutionIndex = 0;
        //loop through each element in the resolutions array
        for (int i = 0; i < resolutions.Length; i++)
        {
            //displays a resolution option 
            string option = resolutions[i].width + "x" + resolutions[i].height;
            //adds the resolution option to our list
            options.Add(option);
            Debug.Log("This option is " + option.ToString());

            //if resolutions is equal to the monitors resolutions (by comparing the width and the height of our resolution)
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                //displays current resolution in index
                currentResolutionIndex = i;
            }
        }
        //adds the list with our resolution options to our dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        uiToggleFull.isOn = true;
        #endregion
        #region Sound (Attempts to get the save sound to play at the start)
        //musicAudio = GameObject.Find("MainMenuMusic").GetComponent<AudioSource>();
        //musicSlider.value = -25f;
        //musicSlider.value = musicAudio.volume;

        //interfaceAudioSlider.value = -25f;
        //musicSlider.value = -25f;
        //musicSlider.value = musicAudio.volume;
        //interfaceAudioSlider.value = interfaceAudio.volume;
        //musicSlider.value = PlayerPrefs.GetFloat("MainMixer", -25f);
        //effectsAudioSlider.value = PlayerPrefs.GetFloat("SoundEffect", -25f);
        #endregion

        #region Brightness
        //temporary color is equal to the current color of the image
        var tempColor = brightnessImage.color;
        //brightness slider value is equal to the alpha value of the temp color
        brightnessSlider.value = 1.0f - tempColor.a;
        #endregion
        //finds the saveFilePath in Application (all main menu data is saved to SaveData/Data as a xml
        saveFilePath = Application.dataPath + "/SaveData/Data/" + fileName + ".xml";
        if (File.Exists(saveFilePath))//if the file path exists
        {
            //calls the load function
            Load();
        }
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
            //displays options menu
            showOptions = true;
            //sets the main menu as inactive
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            //options menu is active
            //Gets the Slider component from the tagged game object music Slider
            musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
            //Gets the  Slider component from the tagged game object Inteface Slider
            interfaceAudioSlider = GameObject.FindGameObjectWithTag("InterfaceEffectsSlider").GetComponent<Slider>();
            //Gets the  dropdown component from the  game object resolution dropdown
            resolutionDropdown = GameObject.Find("ResolutionDropDown").GetComponent<Dropdown>();
            //Gets the  dropdown component from the  game object graphics dropdown
            graphicsDropdown = GameObject.Find("QualityDropDown").GetComponent<Dropdown>();
            //Gets the  toggle component from the  game object fullscreen toggle
            uiToggleFull = GameObject.Find("FullScreenToggle").GetComponent<Toggle>();
            //Gets the  Slider component from the  game object brightness Slider
            brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
            var tempColor = brightnessImage.color;
            //the brightess of the image via it's alpha color
            brightnessSlider.value = 1.0f - tempColor.a;
            return false;


        }
    }
    //music Audio Mixer controller
    public void MusicVolume(float musicVolume)
    {
        //sets the volume for the musicAudioMixer
        musicAudioMixer.SetFloat("MusicVolume", musicVolume);
    }
    //Interface Audio Mixer controller
    public void InterfaceVolume(float interfaceVolume)
    {
        //sets the volume for the interfaceAudioMixer
        interfaceAudioMixer.SetFloat("InterfaceVolume", interfaceVolume);

    }

    //Visual Quality function
    public void SetQuality(int qualityIndex)
    {
        //Sets the visual Quality* by obtaining the Quality settings set in Unity project settings as a index.
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    //change brightness function
    public void ChangeBrightness()
    {
        //temporary color is equal to the brightnessImage's color
        var tempColor = brightnessImage.color;
        //changes the alpha of the temp color minus the slider value
        tempColor.a = 0.5f - brightnessSlider.value;
        //new color of the image is equal to the temp color
        brightnessImage.color = tempColor;

        Debug.Log("Bright");
    }

    //Sets the resloution in the resolution index (int value)
    public void SetResolution(int resolutionIndex)
    {
        //Obtains and displays available resolutions from the resolution  index
        Resolution resolution = resolutions[resolutionIndex];
        //Sets the selected resolution in the resolution index by its width and height. Also sets it as fullscreen.
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Toggles between windowed and fullscreen
    public void ToggleFullScreen(bool isFullscreen)
    {
        //toggles fullscreen mode via bool in event system.
        if (uiToggleFull.isOn == true)
        {
            //the screen is now fullscreen
            Screen.fullScreen = isFullscreen;
        }
        else
        {
            //if it's not in fullscreen then it is in windowed.
            Screen.fullScreen = !isFullscreen;
        }


    }
    //Save Data Function 
    public void Save()
    {
        //the bool fullscreen pref is saving the UIManager's toggleFullScreen bool
        data.fullscreenPref = uiToggleFull.isOn;
        //the saved Resolution is equal to the resolution drop down value
        data.savedResolution = resolutionDropdown.value;
        //brightness slider float is equal to the brightness slider value in UI Manager
        data.brightnessSlider = brightnessSlider.value;
        //quality int is equal to the value selected in the graphics dropdown
        data.qualityIndex = graphicsDropdown.value;
        //saved float of the music volume is equal to the musicSlider.value
        data.savedMusicVolume = musicSlider.value;
        //data saved Interface Volume int is equal to the audioslider value
        data.savedInterfaceVolume = interfaceAudioSlider.value;
        //new xml serializer containing the data types in SavedSettingsData
        var serializer = new XmlSerializer(typeof(SavedSettingsData));
        using (var stream = new FileStream(saveFilePath, FileMode.Create))//creates a file in the saved File Path
        {
            //serialize the filestream and the data
            serializer.Serialize(stream, data);
        }

    }
    //Load Data Function 
    public void Load()
    {
        //new xmlSerialiser containing the data types in SavedSettingsData
        var serializer = new XmlSerializer(typeof(SavedSettingsData));
        using (var stream = new FileStream(saveFilePath, FileMode.Open))//opens the save file in the filestream path 
        {
            data = serializer.Deserialize(stream) as SavedSettingsData;
        }
        //the music Slider value is equal to the savedMusicVolume Float
        musicSlider.value = data.savedMusicVolume;
        //interface Audio Slider value is equal to the data saved Interface float
        interfaceAudioSlider.value = data.savedInterfaceVolume;
        //the togglefullscreen bool is equal to the bool of fullscreen pref in data
        uiToggleFull.isOn = data.fullscreenPref;
        //value of the brightness slider is equal to the float of the brightness slider variable in data
        brightnessSlider.value = data.brightnessSlider;
        //value of the resolution dropdown is equal to the int of saved resolution
        resolutionDropdown.value = data.savedResolution;
        //value of graphics drop down is equal to the quality index
        graphicsDropdown.value = data.qualityIndex;
    }
}
