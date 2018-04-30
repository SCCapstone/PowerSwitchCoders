using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour {

    public PathHandler pathHandler;
    public PowerPointUpdate powerPointPanel;
    public PathRunner pathRunner;
    public GameObject playButton;
    public GameObject playerVeh;
    public GameObject FailScreen;
    public GameObject WinScreen;
    public GameObject audioHandler;
    public int unlockNumber;
    public GameObject questionsPanel;
    public GameObject[] buttons;

    private AudioSource audioSource;
    private bool done = false;

    // Use this for initialization
    /// <summary>
    /// Init the audio source
    /// </summary>
    void Start () {
        audioSource = audioHandler.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if (!done && powerPointPanel.LostGame())
        {
            FailScreen.SetActive(true);
            audioSource.Stop();
            audioSource.PlayOneShot((AudioClip)Resources.Load("Sound/BadStuff/Sample1"));
            done = true;
        }
        if (!done && pathRunner.WinGame())
        {
            if (PlayerPrefs.GetInt("unlock", 0) < unlockNumber)
                PlayerPrefs.SetInt("unlock", unlockNumber);

            questionsPanel.GetComponent<Questions>().askQuestion();

            WinScreen.SetActive(true);            
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.PlayOneShot((AudioClip)Resources.Load("Sound/Winning/Sample1"));
            
            Time.timeScale = 0.0f;
            done = true;
        }
	}

    //Play Level Function, checks if the level is ready to start
    public void PlayLevel()
    {
        if (pathHandler.CheckStart())
        {
            powerPointPanel.DoUpdate();
            pathRunner.PowerSwitch();
            playButton.SetActive(false);
            ButtonsOff();
            playerVeh.SetActive(true);

        }
    }

    public void WinLevel()
    {
        WinScreen.SetActive(true);
        Debug.Log("You win");
        
    }

    public void ButtonsOff()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }
}
