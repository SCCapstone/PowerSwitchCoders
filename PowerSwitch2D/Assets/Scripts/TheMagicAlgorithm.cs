using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheMagicAlgorithm : MonoBehaviour {

    [Header("Power Source Label Text & Int Values")]
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

    private string[] vehicleInts = { "Dummy", "Bike", "Car", "Boat", "Train" };
    private string[] pathJoints = { "AB", "BC", "CD", "DE", "EF", "FG" };
    private string[] fuelTypes = { "Dummy", "man", "oil", "wind", "coal" };
    //will therefore need a checkUpgrades function 

    private int pCursor = 0;

    [Header("Number of Power Points")]
    [Tooltip("Must be greater than zero")]

    public int powerPoints = 100;

    [Header("Number of Choices = playerPaths size")]
    [Tooltip("Determines size of array playerPaths")]

    public MovementPath[] playerPaths;
    private int[] playerFuels;

    public string usertest { private get; set; }
    //private string[] powerSources;

    //private MovementPath[] possPaths;

    //[Header("Movement Paths")]
    //[Tooltip("This array needs to be filled with each and every movement path in the level")]

    //deprecated ftm 
    //public MovementPath[] AllPaths;

    // Use this for initialization
    void Start () {

        RandomizeCosts();
        playerFuels = new int[playerPaths.Length];
        //powerSources = new string[playerPaths.Length];
        //pCursor = 1;

        //int tempSize = playerPaths.Length;

        //Possible 4 paths per chioce so 4 * number of choices
        //possPaths = new MovementPath[tempSize];
    }
	
    //Need IF(PowerPoints > (PowerSourceCost*(Distance/Speed) Then VALID
    //ELSE FAIL

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

    //Player fills the array of their chosen paths as they pick a vehicle at each point 
    public void PickPath(int choice)
    {
        string vPick = vehicleInts[choice];
        //Debug.Log(vPick);


        MovementPath nextPath = PathFinder(vPick);
        playerPaths.SetValue(nextPath, pCursor);

        string fuelChoice = fuelTypes[choice];
        int nextFuel = FuelSetter(fuelChoice);
        playerFuels.SetValue(nextFuel, pCursor);
        Debug.Log(fuelChoice+nextFuel);

        //int nextFuel = FuelSetter(fuel);
        //playerFuels.SetValue(nextFuel, pCursor);
        

        pCursor++;
        if (pCursor >= playerPaths.Length)
        {
            Debug.Log("Picked Max Number of Points");
            CheckWinningPath();
            //Debug.Log()
            //Player is done picking, calculate if their path will win or not using above IF
            //Also activate car sprite and set to correct value based on path, but don't move it yet
        }
    }

    //Frontload instead?
    MovementPath PathFinder(string currentMover)
    {
        if (currentMover == null)
        {
            Debug.Log("Current Vehicle Mover (ie Car) is NULL");
            return null;
        }

        string searchPath = currentMover + "Path" + pathJoints[pCursor];
        GameObject tempPath = GameObject.Find(searchPath);
        if (tempPath == null)
        {
            Debug.Log("Failed to find path with that name");
            return null;
        }
        MovementPath newMove = tempPath.GetComponent<MovementPath>();
        return newMove;
    }

    //This.... not great 
    int FuelSetter(string fuelType)
    {
        if (fuelType == null)
        {
            Debug.Log("Power Type Not Set");
            return 0;
        }

        if (fuelType == "man")
        {
            return manCost;
        }

        if (fuelType == "wind")
        {
            return windCost;
        }

        if (fuelType == "electric")
        {
            return electricCost;
        }

        if (fuelType == "oil")
        {
            return oilCost;
        }

        if (fuelType == "coal")
        {
            return coalCost;
        }

        //BAD 
        return 0;
    }

    void CheckWinningPath()
    {
        float playerCost = 0;
        for (int i = 0; i < playerPaths.Length; i++)
        {
            playerCost += playerFuels[i] * (playerPaths[i].travelDistance / playerPaths[i].travelSpeed);
        }
        if ( Mathf.Floor(playerCost) > powerPoints)
        {
            Debug.Log("This path will FAIL");
            Debug.Log(playerCost);
        } else
        {
            Debug.Log("This path will PASS");
            Debug.Log(playerCost);
        }
            
    }

}
