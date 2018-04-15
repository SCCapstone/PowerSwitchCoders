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
    private AudioSource audioSource;
    private bool done = false;

    // Use this for initialization
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
            WinScreen.SetActive(true);
            //powerPointPanel.gameObject.SetActive(false);
            
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.PlayOneShot((AudioClip)Resources.Load("Sound/Winning/Sample1"));
            
            //audioSource.pl
            Time.timeScale = 0.0f;
            done = true;
        }
	}

    public void PlayLevel()
    {
        if (pathHandler.canStart)
        {
            powerPointPanel.DoUpdate();
            pathRunner.PowerSwitch();
            playButton.SetActive(false);
            playerVeh.SetActive(true);

        }
    }

    public void WinLevel()
    {
        WinScreen.SetActive(true);
        Debug.Log("You win");
        
    }
}
