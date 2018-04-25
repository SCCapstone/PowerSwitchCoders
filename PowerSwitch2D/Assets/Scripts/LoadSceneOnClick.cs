using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    private Button myButton;
    public int unlockNumber;

    void Start()
    {
        myButton = GetComponent<Button>();

        if (PlayerPrefs.GetInt("unlock", 0) < unlockNumber)
            myButton.interactable = false;
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


}
