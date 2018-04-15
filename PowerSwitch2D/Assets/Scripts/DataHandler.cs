using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataHandler : MonoBehaviour {

    [Header("Power Source Label Text & Int Values")]
    [Tooltip("These values control the costs of the Power Sources")]

    public Text manText;
    public Text windText;
    public Text electricText;
    public Text oilText;
    public Text coalText;

    public int manCost = 10;
    public int windCost = 15;
    public int electricCost = 20;
    public int oilCost = 30;
    public int coalCost = 35;

    //Variance should never be larger than half the value of the cheapest powersource, or a cost might be set < 0.
    public int variance = 5;

    [Header("Number of Power Points")]
    [Tooltip("Must be greater than zero")]

    public int powerPoints = 100;

    // Use this for initialization
    private void Awake()
    {
        RandomizeCosts();
    }

    void Start () {
        //Awake is called first
        //RandomizeCosts();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void RandomizeCosts()
    {
        manCost += Random.Range(-variance, variance);
        windCost += Random.Range(-variance, variance);
        electricCost += Random.Range(-variance, variance);
        oilCost += Random.Range(-variance, variance);
        coalCost += Random.Range(-variance, variance);

        //
        manText.text = manCost.ToString();
        windText.text = windCost.ToString();
        electricText.text = electricCost.ToString();
        oilText.text = oilCost.ToString();
        coalText.text = coalCost.ToString();
    }

    public void updatePowerPoints(int newValue)
    {
        powerPoints = newValue;
    }

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
