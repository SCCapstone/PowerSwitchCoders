using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour {

    private int manCost;
    private int windCost;
    private int electricCost;
    private int oilCost;
    private int coalCost;

    

    public DataHandler costData;

    [Header("Number of Choices = playerPaths size")]
    [Tooltip("Determines size of array playerPaths")]

    public MovementPath[] playerPaths;

    [Header("Live Data - Don't Touch These Values")]
    [Tooltip("These values control the costs of the Power Sources")]

    public float pickedPathCost; 

    //Variance should never be larger than half the value of the cheapest powersource, or a cost might be set < 0.
    //private int variance;

    private string[] vehicleInts = { "Dummy", "Bike", "Car", "Boat", "Train", "Plane"};
    private string[] pathJoints = { "AB", "BC", "CD", "DE", "EF", "FG" };
    private string[] fuelTypes = { "Dummy", "man", "oil", "wind", "coal", "electric"};
    //will therefore need a checkUpgrades function 

    private int pCursor = 0;
    public int powerPoints;

    
    private int[] playerFuels;
    private float trueDistance;

    //GIZMOS
    public void OnDrawGizmos()
    {
        if (!(costData == null))
        {
            Gizmos.DrawLine(costData.transform.position, this.transform.position);
        } else
        {
            return;
        }
    }

    private void Awake()
    {
        powerPoints = costData.powerPoints;
    }

    // Use this for initialization
    void Start(){

        playerFuels = new int[playerPaths.Length];

        //powerPoints = costData.powerPoints;

        manCost = costData.manCost;
        windCost = costData.windCost;
        electricCost = costData.electricCost;
        oilCost = costData.oilCost;
        coalCost = costData.coalCost;
    }


	void Update () {
        //do PowerPoints text value update here, along with possible other effects, and win/fail handling
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
        AddTravelTime(newMove);
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

    public bool CheckWinningPath()
    {
        float playerCost = 0;
        for (int i = 0; i < playerPaths.Length; i++)
        {
            playerCost += playerFuels[i] * (playerPaths[i].travelDistance / playerPaths[i].travelSpeed);
            pickedPathCost = playerCost;
        }
        if ( Mathf.Floor(playerCost) > powerPoints)
        {
            Debug.Log("This path will FAIL");
            Debug.Log(playerCost);
            return false;
        } else
        {
            Debug.Log("This path will PASS");
            Debug.Log(playerCost);
            return true;
        }
            
    }

    void AddTravelTime(MovementPath travelPath)
    {
        //
        for (int i = 1; i < travelPath.PathSequence.Length; i++)
        {
            Vector3 aLoc = travelPath.PathSequence[i].transform.position;
            Vector3 bLoc = travelPath.PathSequence[i - 1].transform.position;
            //Get the time it takes to travel between each point
            trueDistance += (Vector3.Distance(aLoc, bLoc) / travelPath.travelSpeed);
        }
    }

    public float GetTravelTime()
    {
        return trueDistance;
    }

}
