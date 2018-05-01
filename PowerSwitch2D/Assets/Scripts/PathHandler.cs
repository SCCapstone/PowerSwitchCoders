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
    public bool canStart = false;

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
        Time.timeScale = 1.0f;
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

        pCursor++;
        if (pCursor >= playerPaths.Length)
        {
            //Debug.Log("Picked Max Number of Points");
            CheckWinningPath();
            canStart = true;
            //Debug.Log()
            //Player is done picking, calculate if their path will win or not using above IF
            //Also activate car sprite and set to correct value based on path, but don't move it yet
        }
    }

    //Newest PickPath function, allowing choices to be changed, at suggestion of QA
    public void PickPathNew(MovementPath newPath)
    {
        if (newPath != null)
        {
            string choice = newPath.pathJoint.ToString();
            for (int i = 0; i < pathJoints.Length; i++) 
            {
                if (choice == pathJoints[i])
                {
                    if (i <= playerPaths.Length)
                    {
                        //If the path matches a valid one and isnt' null, add it to the player's array of choices
                        playerPaths.SetValue(newPath, i);
                        pCursor++;
                        break;
                    } else
                    {
                        Debug.Log("Value Too Large");
                    }
                }
            }
        }
        
    }

    //Check if game can start
    public bool CheckStart()
    {
        foreach (MovementPath path in playerPaths)
        {
            if (path == null)
            {
                pickedPathCost = 0;
                return false;
            }
            AddTravelTime(path);
            pickedPathCost += CheckCost(path.travelDistance, path.travelSpeed);
        }
        //Debug.Log(pickedPathCost);
        //Debug.Log(trueDistance);
        return true;
    }

    //Deprecated code used to locate a particular picked path via the GameObject.Find function (very perf heavy)
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

    //Deprecated code to translate the player's path choice fuel type
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

        //Critical Failure
        return 0;
    }

    //Check if the player has picked a winning combination of paths
    public bool CheckWinningPath()
    {
        float playerCost = pickedPathCost;
        //Debug.Log(playerCost);
        if ( Mathf.Floor(playerCost) > powerPoints)
        {
            //Debug.Log("This path will FAIL");
            return false;
        } else
        {
            //Debug.Log("This path will PASS");
            return true;
        }
        
            
    }

    //Check the cost of each path
    public float CheckCost(int dist, float speed)
    {
        return ((dist+0.0f) / speed)*10.0f;
    }

    //Calculate the total real travel time (Unity uses meters/second) that the vehicle will be moving
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
