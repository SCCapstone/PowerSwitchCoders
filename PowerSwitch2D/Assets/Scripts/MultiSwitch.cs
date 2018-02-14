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

    //ToDo: Make this code less horrible and break-prone
	void Start () {
        currentFP = this.gameObject.GetComponent<FollowPath>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchSpriteAndPath ()
    {
        currentFP.MyPath = newMovePath;
        Sprite switchSprite = linkedVehicle.GetComponent<Sprite>(); 
        switchSprite = newVehicle;
    }
}
