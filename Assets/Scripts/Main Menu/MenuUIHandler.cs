using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    #region Variables
    #region Audio Refs
    [Header("Audio")]
    public AudioSource musicAudio;
    public AudioSource menuAudio, gameAudio;
    public Slider musicSlider, gameAudioSlider, effectsAudioSlider;
    public float volume;
    public AudioMixer masterMixer, effectsMixer;
    #endregion
    #region Visual Refs
    [Header("Visuals")]
    public Resolution[] resolutions;
    public Dropdown resolutionDropdown, graphicsDropdown;
    public Slider brightnessSlider;
    public Light lightSource;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public bool showOptions;
    #endregion
    #endregion
    void Start()
    {
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
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        #endregion
        #region Sound
        musicAudio = GameObject.Find("MainMenu Music").GetComponent<AudioSource>();
        musicSlider.value = -25f;
        musicSlider.value = musicAudio.volume;
        
        effectsAudioSlider.value = -25f;
        //musicSlider.value = PlayerPrefs.GetFloat("MainMixer", -25f);
        //effectsAudioSlider.value = PlayerPrefs.GetFloat("SoundEffect", -25f);
        #endregion

        #region Brightness
        //brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
        #endregion

    }


    void Update()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void ExitGame()
    {
        Debug.Log("Quit it");
        Application.Quit();
    }
    public void ToggleOptions()
    {
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
            //
            return true;
            //
        }
        else
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
            effectsAudioSlider = GameObject.FindGameObjectWithTag("InterfaceEffectsSlider").GetComponent<Slider>();
            resolutionDropdown = GameObject.Find("ResolutionDropDown").GetComponent<Dropdown>();
            graphicsDropdown = GameObject.Find("QualityDropDown").GetComponent<Dropdown>();
            //volumeSlider.value = mainAudio.volume;

            brightnessSlider = GameObject.FindGameObjectWithTag("BrightnessSlider").GetComponent<Slider>();

            //brightnessSlider.value = dirLight.intensity;
            return false;


        }
    }
    public void MasterVolume (float volume)
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
    public void UpdateSliders()
    {
        float musicValue = PlayerPrefs.GetFloat("Volume");

        musicSlider.value = musicValue;
    }
    public void EffectsVolume(float volume)
    {

        effectsMixer.SetFloat("SoundEffects", volume);
    }
    //Visual Quality function
    public void SetQuality(int qualityIndex)
    {
        //Sets the visual Quality of Graphics by obtaining the graphics settings in Unity.
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void ChangeBrightness()
    {


        Debug.Log("Bright");
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MainMixer", musicSlider.value);
        PlayerPrefs.SetFloat("SoundEffect", effectsAudioSlider.value);

    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void ToggleFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

    }
}
