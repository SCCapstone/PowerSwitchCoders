using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject scoreText;
    public string levelHandle;

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

            //Ask a question on win of enabled.
            if(PlayerPrefs.GetInt("questions",1)==1)
                questionsPanel.GetComponent<Questions>().askQuestion();

            //Handle score
            HandleScore();

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

    public void HandleScore()
    {
        //initial values
        int finalScore = 0;
        int topScore = 0;
        bool isCorrect = questionsPanel.GetComponent<Questions>().isCorrect();
        int finalPoints = powerPointPanel.getPowerPoints();

        //determine final score
        if (isCorrect)
            finalScore = (int)(finalPoints + (finalPoints * 0.25)) * 10;
        else
            finalScore = finalPoints * 10;

        //Determine top score
        int oldTopScore = PlayerPrefs.GetInt(levelHandle, 0);
        if (finalScore > oldTopScore)
        {
            PlayerPrefs.SetInt(levelHandle, finalScore);
            topScore = finalScore;
        }
        else
            topScore = oldTopScore;


        scoreText.GetComponent<Text>().text = "Your Score: " + finalScore.ToString() + "\nTop Score: " + topScore.ToString();
    }
}
