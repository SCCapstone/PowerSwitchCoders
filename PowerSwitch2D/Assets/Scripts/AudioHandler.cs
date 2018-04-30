using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioHandler : MonoBehaviour {


    public Slider VolumeSlider;
    public AudioSource Music;
    private GameObject hitOBJ;
	// Use this for initialization
	void Start () {
        VolumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
        //Check if player has already set volume before, and if so, adjust music to that volume
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            //Set the Music Volume and current Game Volume
            Music.volume = VolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            AudioListener.volume = VolumeSlider.value;  //Control Global game volume
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Detect any and all button clicks
        if (Input.GetButtonDown ("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //Retrieve struck object
                hitOBJ = EventSystem.current.currentSelectedGameObject;
                //Check we hit a button, if so, play the button click sound
                if (hitOBJ != null && hitOBJ.GetComponent<Button>() != null)
                {
                    if (hitOBJ.GetComponent<Button>().isActiveAndEnabled)
                    {
                        Music.PlayOneShot((AudioClip)Resources.Load("Sound/UISounds/ButtonPress"));
                    }
                    else
                    {
                        //Play error sound
                    }
                }
            }
        }
    }

    //Update volume based on volume slider
    public void UpdateVolume ()
    {
        Music.volume = VolumeSlider.value;
        AudioListener.volume = VolumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", VolumeSlider.value);
    }
}
