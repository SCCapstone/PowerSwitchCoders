using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour {


    public Slider VolumeSlider;
    public AudioSource Music;
    //public AudioListener Game;
	// Use this for initialization
	void Start () {
        VolumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
        //Check if player has already set volume before, and if so, adjust music to that volume
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            Music.volume = VolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            AudioListener.volume = VolumeSlider.value;  //Control Global game volume
        }
	}
	
	// Update is called once per frame
	void Update () {
		//Music.volume = 
	}

    public void UpdateVolume ()
    {
        Music.volume = VolumeSlider.value;
        AudioListener.volume = VolumeSlider.value;
        //Possibly move this "save preferred volume" code to when user exits the settings menu
        PlayerPrefs.SetFloat("MusicVolume", VolumeSlider.value);
    }
}
