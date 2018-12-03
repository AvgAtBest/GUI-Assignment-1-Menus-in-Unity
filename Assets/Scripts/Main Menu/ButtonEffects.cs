using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Class for button effects. PointerEnterHandler and PointerDownHandler allows for the mouse to interact with the button when the mouse hovers over or clicks on the button
public class ButtonEffects : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler
{
    //audiosource of the button
    public AudioSource buttonAudio;
    //audio clip index that is played
    public AudioClip[] soundEffects;

	void Start ()
    {
        //gets the component of the audio source from the gameobject
        buttonAudio = GameObject.Find("ButtonSounds").GetComponent<AudioSource>();
	}
	
    //Function that plays a sound when the mouse hovers over the button
    public void OnPointerDown(PointerEventData ped)
    {
        //the audiosource gets a sound effect clip in the sound effects index
        buttonAudio.clip = soundEffects[0];
        //plays the sound effect
        buttonAudio.Play();
    }
    //Functions that plays the sound effect in index when the mouse button clicks the UI button
    public void OnPointerEnter(PointerEventData ped)
    {
        //the audiosource gets a sound effect clip in the sound effects index
        buttonAudio.clip = soundEffects[1];
        //plays the sound effect
        buttonAudio.Play();
    }
}
