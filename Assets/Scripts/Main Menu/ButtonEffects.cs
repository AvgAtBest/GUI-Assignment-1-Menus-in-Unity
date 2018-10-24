using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonEffects : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler
{
    public AudioSource buttonAudio;
    public AudioClip[] soundEffects;

	void Start ()
    {
        buttonAudio = GameObject.Find("ButtonSounds").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void OnPointerDown(PointerEventData ped)
    {
        buttonAudio.clip = soundEffects[0];
        buttonAudio.Play();
    }
    public void OnPointerEnter(PointerEventData ped)
    {
        buttonAudio.clip = soundEffects[1];
        buttonAudio.Play();
    }
}
