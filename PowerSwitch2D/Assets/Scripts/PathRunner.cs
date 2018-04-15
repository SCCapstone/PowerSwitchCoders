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

    [Header("Explosion Effect")]
    [Tooltip("Boom")]
    public GameObject boom;
    private bool hasWon = false;

    // Use this for initialization
    void Start () {
        pathCursor = 0;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Time.time + nextUpdate;
            //FuelBurnTest();
        }
        */
    }

    void FuelBurnTest()
    {
        int points = dataHandler.powerPoints;
        points -= 5;
        dataHandler.updatePowerPoints(points);
    }

    /*
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
    */
    public void PowerSwitch()
    {
        if (pathCursor >= pathHandler.playerPaths.Length)
        {
            int lastPath = pathHandler.playerPaths.Length;
            newPath = pathHandler.playerPaths[lastPath-1];
            hasWon = true;
            if (newPath == null)
            {
                Debug.Log("Critical Failure");
            }
            playerMover.MyMovementPath = newPath;
            playerMover.Speed = 0;
        } else
        {
            //playerMover.Speed = 0;
            Explode();
            newPath = pathHandler.playerPaths[pathCursor];
            playerMover.MyMovementPath = newPath;
            playerSprite.sprite = newPath.linkedSprite;
            playerMover.Speed = newPath.travelSpeed;
            playerMover.Start();
            pathCursor++;
        }

        

    }

    public bool WinGame()
    {
        //either win or lose, true or false
        //Time.timeScale = 0.0f;
        return hasWon;
    }

    public void Explode()
    {
        //Make Boom
        GameObject newBoom = Instantiate(boom, playerMover.transform.position, Quaternion.identity);
        //Destroy Boom
        Destroy(newBoom, 2.0f);
    }
}
