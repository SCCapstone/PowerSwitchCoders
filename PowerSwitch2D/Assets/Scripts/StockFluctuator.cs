using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockFluctuator : MonoBehaviour {

    //Public variables visible in editor; will be customized per-level but come pre-defined

    public int manpowerCost = 10;
    public int windCost = 15;
    public int electricCost = 20;
    public int oilCost = 30;
    public int coalCost = 35;

    //Linked to the actual in-game Text objects
    public Text manpowerText;
    public Text windText;
    public Text electricText;
    public Text oilText;
    public Text coalText;


	// Use this for initialization
	void Start () {
        //Move to random update function
        manpowerText.text = manpowerCost.ToString();
        windText.text = windCost.ToString();
        electricText.text = electricCost.ToString();
        oilText.text = oilCost.ToString();
        coalText.text = coalCost.ToString();

        //Randomized delays and values so that "fixed" nature of fluctuation is less apparent - currently not working due to technical jargon
        //Rework this into a COROUTINE so that it functions better and is easier to manage/debug
        InvokeRepeating("FluctuatePrices", 0.5f, 2.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //Replace with either coroutines or 5 seperate InvokeRepeating calls for basic randomness
    void FluctuatePrices ()
    {
        Fluctuate(manpowerText, manpowerCost);
        Fluctuate(windText, windCost);
        Fluctuate(electricText, electricCost);
        Fluctuate(oilText, oilCost);
        Fluctuate(coalText, coalCost);

    }

    //Currently NOT storing the random (stock market) values of individual power sources - ToDo - need encapsulate function to handle return calls for this
    void Fluctuate(Text textGO, int approxCost)
    {
        int randomNumber = Random.Range(approxCost - 2, approxCost + 3);
        
        //Up/Down arrow activation/deactivation - join into single arrow that gets colored/flipped every time price "toggles" 

        GameObject upArrow = textGO.gameObject.transform.GetChild(0).gameObject;
        GameObject downArrow = textGO.gameObject.transform.GetChild(1).gameObject;
        //Check to see if rising or falling, adjust color appropriately
        if (randomNumber >= approxCost)     //NEW price is greater than/equal to old, which is bad! red and green are built-in but can be adjusted
        {
            textGO.color = Color.red;
            textGO.text = (Mathf.RoundToInt(randomNumber)).ToString();
            upArrow.SetActive(true);
            downArrow.SetActive(false);
        }
        else
        {
            textGO.color = Color.green;
            textGO.text = (Mathf.RoundToInt(randomNumber)).ToString();
            downArrow.SetActive(true);
            upArrow.SetActive(false);

        }
    }
}
