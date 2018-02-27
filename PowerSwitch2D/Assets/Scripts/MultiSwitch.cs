using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSwitch : MonoBehaviour {

    private FollowPath currentFP;
    public MovementPath oldMovePath;
    public MovementPath newMovePath;
    public GameObject linkedVehicle;
    public Sprite newVehicle;

	// Use this for initialization

    //ToDo: Make this code less horrible and error-prone
	void Start () {
        currentFP = this.gameObject.GetComponent<FollowPath>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchSpriteAndPath ()
    {
        currentFP.MyPath = newMovePath;
        linkedVehicle.GetComponent<SpriteRenderer>().sprite = newVehicle;
        linkedVehicle.transform.localScale += new Vector3(1.0f,1.0f,1.0f);
        //linkedVehicle.transform.Rotate(new Vector3(0,0,90f));
        currentFP.Start();
        //Sprite switchSprite = linkedVehicle.GetComponent<SpriteRenderer>().sprite; 
        //switchSprite = newVehicle;
    }
}
