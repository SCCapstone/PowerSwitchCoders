using System.Collections;
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
        //String array of all factoids in the game
        factArray = new string[] {"The Earth orbits around the Sun", "The fastest growing type of energy is Renewable Energy", "Power is the rate at which energy is converted"
        , "Wind and Solar Energy are Renewable Energies", "Energy from food is measured in Calories or Joules", "The Sun's rays take 8 minutes to reach Earth",
            "In 2010, heating & cooling used the most energy in American homes", "In 2015, the largest source of electrical energy in the US was Coal",
            "Approximately 28% of the world's coal reserves are located within the US", "The world's largest solar power plant is located in the Mojave Desert, USA",
            "Wind Turbines need 4-12mph winds to produce energy", "Heat is a type of energy", "Global Warming is caused by excess Carbon Dioxide",
            "Energy cannot be created or destroyed, only transferred" };
	}

    // Update is called once per frame
    void Update() {
        //Will only activate after 15 seconds of game time, while the Fact Screen is off, and with the popupRate factored in as a delay between fact popups

        //If enough time has passed, start activating fact screen
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
