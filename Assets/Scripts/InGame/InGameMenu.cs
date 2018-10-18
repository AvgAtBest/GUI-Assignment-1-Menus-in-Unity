using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    #region Variables
    [Header("UI Elements")]
    public GameObject gameMenu;

    public bool paused;
    #region Visuals
    [Header("Visuals")]
    public Resolution[] resolutions;
    public Dropdown resolutionDropdown, graphicsDropdown;
    public Slider brightnessSlider;
    public Light lightSource;

    public GameObject optionsMenu;
    public bool showOptions;
    #endregion
    #region Audio
    [Header("Audio")]
    public AudioSource musicAudio;
    public AudioSource menuAudio, gameAudio;
    public Slider musicSlider, effectsAudioSlider;
    public float volume;
    public AudioMixer masterMixer, effectsMixer;
    #endregion
    #endregion
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(false);
        paused = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1;
                gameMenu.SetActive(false);
                //playerUI.SetActive(true);
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                gameMenu.SetActive(true);
                //playerUI.SetActive(false);
                paused = true;
            }
        }
    }
    bool OptionToggle()
    {
        if (showOptions)//showOptions == true or showOptions is true
        {
            showOptions = false;
            //Set to not display Options Menu as it is not actived
            gameMenu.SetActive(true);
            //Show Main Menu as Options is not being viewed
            optionsMenu.SetActive(false);
            //
            return true;
            //
        }
        else
        {
            showOptions = true;
            gameMenu.SetActive(false);
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
        //Sets the selected resolution in resolution index
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void ToggleFullScreen(bool isFullscreen)
    {
        //toggles fullscreen mode via bool in event system.
        Screen.fullScreen = isFullscreen;

    }
    public void Resume()
    {
        paused = false;
        gameMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
