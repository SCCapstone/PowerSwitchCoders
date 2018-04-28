using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventHandler : EventTrigger {

    public AudioSource audioSource;
    public GameObject PressCatcher;
    

	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("Please connect speakers now");
        }
	}

    public override void OnPointerClick(PointerEventData data)
    {
        Debug.Log("OnPointerClick called.");
        PressCatcher = data.rawPointerPress;
        if (PressCatcher.GetComponent<Button>() != null)
        {
            audioSource.PlayOneShot((AudioClip)Resources.Load("Sound/UISounds/ButtonPress"));
            Debug.Log(PressCatcher.GetType());
        }
    }

    public void CheckInput(GameObject hitObj)
    {
        if( hitObj.GetComponent<Button>() != null)
        {
            audioSource.PlayOneShot((AudioClip)Resources.Load("Sound/UISounds/ButtonPress"));
            Debug.Log(PressCatcher.GetType());
            //if (EventSystem.current.IsPointerOverGameObject)
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
