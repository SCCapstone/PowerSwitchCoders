using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleButton : MonoBehaviour {

    public Button playButton;

	// Use this for initialization
	void Start () {
        Button btn = playButton.GetComponent<Button>();
        btn.onClick.AddListener(ClickButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickButton()
    {
        Debug.Log("You have clicked the button!");
    }
}
