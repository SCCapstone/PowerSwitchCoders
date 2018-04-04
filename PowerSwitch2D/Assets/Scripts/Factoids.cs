﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factoids : MonoBehaviour {

    //public bool UseQuestions;
    public GameObject FactScreen;
    public Text FactText;
    //public int maxQuestions;

    //Every x seconds (for example 10.0f = every 10 seconds) a popup MIGHT appear
    public float popupRate = 10.0f;

    private int currentTotal;
    private float nextCheckTime;
    private string[] factArray;
    //public bool GameStarted = false;

	// Use this for initialization
	void Start () {
        //ToDo: supplement this with streamreader so facts can be dynamically read in from a text file and stored in this array
        //Unity/C# StreamReader info: https://support.unity3d.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-
        //Further Info: https://msdn.microsoft.com/en-us/library/system.io.streamreader(v=vs.110).aspx
        //TextAsset: https://docs.unity3d.com/ScriptReference/TextAsset.html
        factArray = new string[] {"The Earth orbits around the Sun", "The fastest growing source of energy is Renewable Energy", "Power is the rate that energy is converted"};
        //Debug.Log(Time.time);
	}

    // Update is called once per frame
    void Update() {
        //Random Number With 1/3 Chance of Popping up a question (theoretically)
        //Will only activate after 15 seconds of game time, while the Fact Screen is off, and with the popupRate factored in as a delay between fact popups
        //Debug.Log(Time.time);

        //if (Time.time > nextCheckTime && Time.time >= 5.0f && 0.1 == Random.Range(0.0f,0.1f) && FactScreen.activeSelf == false)
        if (Time.time > nextCheckTime && Time.time >= 15.0f && FactScreen.activeSelf == false)
        {
            nextCheckTime = Time.time + popupRate;
            int randFact = Random.Range(0, factArray.Length); //btw 0 and last index of array, inclusive
            FactText.text = factArray[randFact];
            //Look into animated slide-up of Fact Screen (from bottom edge) 
            FactScreen.SetActive(true);
        }

        //If the fact has been up for popupRate seconds, turn it off, and set the next possible activation time to this current time + however many seconds
        if (Time.time > nextCheckTime && FactScreen.activeSelf == true)
        {
                nextCheckTime = Time.time + ( (popupRate * 0.5f) + Random.Range(1.0f,3.0f) );
                FactScreen.SetActive(false);
        }
	}
}