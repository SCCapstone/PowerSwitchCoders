using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRunner : MonoBehaviour {

    [Header("Linked Path Handler")]
    [Tooltip("Stores the player's choices")]
    public PathHandler pathHandler;
    public DataHandler dataHandler;
    public FollowPath playerMover;
    public SpriteRenderer playerSprite;
    private int pathCursor;
    private MovementPath newPath;
    private float nextUpdate = 0.05f;

	// Use this for initialization
	void Start () {
        pathCursor = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time >= nextUpdate)
        {
            nextUpdate = Time.time + nextUpdate;
            FuelBurnTest();
        }

    }

    void FuelBurnTest()
    {
        int points = dataHandler.powerPoints;
        points -= 5;
        dataHandler.updatePowerPoints(points);
    }

    public MovementPath OldPowerSwitch()
    {
        
        if (pathCursor >= pathHandler.playerPaths.Length)
        {
            int lastPath = pathHandler.playerPaths.Length;
            newPath = pathHandler.playerPaths[lastPath];
            EndGame(true);
            if (newPath == null)
            {
                Debug.Log("Critical Failure");
            }
            return newPath;
        }

        newPath = pathHandler.playerPaths[pathCursor];
        pathCursor++;
        return newPath;
    }

    public void PowerSwitch()
    {
        if (pathCursor >= pathHandler.playerPaths.Length)
        {
            int lastPath = pathHandler.playerPaths.Length;
            newPath = pathHandler.playerPaths[lastPath-1];
            EndGame(true);
            if (newPath == null)
            {
                Debug.Log("Critical Failure");
            }
            playerMover.MyMovementPath = newPath;
            playerMover.Speed = 0;
        } else
        {
            newPath = pathHandler.playerPaths[pathCursor];
            playerMover.MyMovementPath = newPath;
            playerSprite.sprite = newPath.linkedSprite;
            playerMover.Speed = newPath.travelSpeed;
            playerMover.Start();
            pathCursor++;
        }

        

    }

    public void EndGame(bool hasWon)
    {
        //either win or lose, true or false
    }
}
