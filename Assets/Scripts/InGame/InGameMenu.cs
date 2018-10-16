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
    public GameObject d;

    private bool paused;


    [Header("Audio")]
    public AudioSource musicAudio;
    public AudioSource menuAudio, gameAudio;
    public Slider musicSlider, gameAudioSlider, effectsAudioSlider;
    public float volume;
    public AudioMixer masterMixer, effectsMixer;
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
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void Quit()
    {
        Application.Quit();
    }
}
