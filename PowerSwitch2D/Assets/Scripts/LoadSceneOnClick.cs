using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    //Button and unlock number
    private Button myButton;
    public int unlockNumber;

    void Start()
    {
        //Get the button this script is attached to
        myButton = GetComponent<Button>();

        //For the level select screen, only the buttons for unlocked levels should be active
        if (PlayerPrefs.GetInt("unlock", 0) < unlockNumber)
            myButton.interactable = false;
    }

    //Loads scenes by their index int
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


}
