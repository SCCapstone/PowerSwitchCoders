using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheMagicAlgorithm : MonoBehaviour {

    [Header("Power Source Labels")]
    [Tooltip("These values control the costs of the Power Sources")]

    public Text manText;
    public Text windText;
    public Text electricText;
    public Text oilText;
    public Text coalText;

    public int manCost;
    public int windCost;
    public int electricCost;
    public int oilCost;
    public int coalCost;

    //Variance should never be larger than half the value of the cheapest powersource, or a cost might be set < 0.
    public int variance = 5;
    /*
    [Header("Vehicle Choice Numbers - For PickPath(int)")]
    [Tooltip("If car is first, then PickPath(1) picks Car")]
    
    //public Sprite CarSprite;
    public int bike = 1;
    public int car = 2;
    public int boat = 3;
    public int train = 4;
    */
    private string[] vehicleInts = { "Dummy", "Bike", "Car", "Boat", "Train" };
    private string[] pathJoints = { "Dummy", "AB", "BC", "CD", "DE", "EF" };

    private int pCursor = 0;

    [Header("Number of Choices = playerPaths size")]
    [Tooltip("Determines size of array playerPaths")]

    public MovementPath[] playerPaths;

    //[Header("Movement Paths")]
    //[Tooltip("This array needs to be filled with each and every movement path in the level")]

    //deprecated ftm 
    //public MovementPath[] AllPaths;

    // Use this for initialization
    void Start () {

        RandomizeCosts();
    }
	
    //Need IF(PowerPoints > (PowerSourceCost*(Distance/Speed) Then VALID
    //ELSE FAIL

	// Update is called once per frame
	void Update () {
        //do PowerPoints text value update here, along with possible other effects, and win/fail handling
	}

    void RandomizeCosts ()
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

    void PickPath(int choice)
    {
        pCursor++;
        if (pCursor >= playerPaths.Length)
        {
            //Player is done picking, calculate if their path will win or not using above IF
        }
    }

    MovementPath PathFinder(string currentJoint)
    {
        if (currentJoint == null)
        {
            return null;
        }

        string searchPath = pathJoints[pCursor] + currentJoint;
        GameObject tempPath = GameObject.Find(searchPath);
        if (tempPath == null)
        {
            Debug.Log("Failed to find path with that name");
            return null;
        }
        MovementPath newMove = tempPath.GetComponent<MovementPath>();
        return newMove;
    }
}
