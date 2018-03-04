using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour {

    //Editor-defined variables
    private FollowPath currentPath;
    private bool levelStarted = false;

    //Rest of the hard-coded paths go here for now
    //In future, use same array technique as MovementPath.cs uses to store path points and have it store actual paths as well
    //Then get the actual size(num paths) in the start method with error checking to set up choices
    public MovementPath ABpath;
    public MovementPath BCpath;
    public MovementPath CDpath;

    public int choices;
    private int pickCursor = 0;

    //Next, the vehicle sprites we will be using within this level
    public Sprite carSprite;
    public Sprite bikeSprite;
    public Sprite boatSprite;
    public Sprite scooterSprite;
    private SpriteRenderer currentSprite;

    //Finally, the vehicle game object itself
    public GameObject playerVehicle;

    //As well as the actual Battery indicating the Power Points
    public Slider Battery;
    private int PowerPoints = 100;

    public GameObject FailScreen;
    public GameObject WinScreen;

	// Use this for initialization
    //Set up the connections between the current player vehicle path and sprite
	void Start () {
        currentPath = playerVehicle.GetComponent<FollowPath>();
        currentSprite = playerVehicle.transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (currentPath == null || currentSprite == null)
        {
            Debug.Log("Couldn't find Player Vehicle path or sprite - something went wrong");
        }
        pickCursor = 0;
	}

    public void BeginLevel()
    {
        if (!levelStarted)
        {
            if (pickCursor < choices)
            {
                Debug.Log("Not enough choices made");
            }
            else
            {
                levelStarted = true;
                playerVehicle.SetActive(true);
                currentPath.Speed = ABpath.travelSpeed;
                //InvokeRepeating calls (a particular function, after DELAYf seconds, every NUMBERf seconds)
                //Consider replacing with update function call if battery slider jitteryness becomes an issue
                InvokeRepeating("BurnFuel", 1.0f, 0.5f);
            }
            
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		if (PowerPoints <= 0)
        {
            CancelInvoke("BurnFuel");
            currentPath.Speed = 0;
            FailScreen.SetActive(true);
            levelStarted = false;
        }
	}

    //Rework this to be modular, not hard-coded
    public void PickVehicle(string vehicleName)
    {
        if (pickCursor == -1)
        {
            pickCursor++;
        } else if (pickCursor == 0) {
            
            string tempPathName = vehicleName + "PathAB";
            GameObject tempPath = GameObject.Find(tempPathName);
            if (tempPath == null)
            {
                Debug.Log("Failed to find path with that name");
            }
            MovementPath tempMove = tempPath.GetComponent<MovementPath>();
            
            ABpath = tempMove;
            currentPath.MyPath = ABpath;
            currentSprite.sprite = ABpath.linkedSprite;
            //PickSprite(vehicleName);
            pickCursor++;

        } else if (pickCursor == 1)
        {
            string tempPathName = vehicleName + "PathBC";
            GameObject tempPath = GameObject.Find(tempPathName);
            if (tempPath == null)
            {
                Debug.Log("Failed to find path with that name");
            }
            MovementPath tempMove = tempPath.GetComponent<MovementPath>();

            BCpath = tempMove;
            //currentPath.MyPath = ABpath;
            //PickSprite(vehicleName);
            pickCursor++;
        } else if (pickCursor == 2)
        {
            string tempPathName = vehicleName + "PathCD";
            GameObject tempPath = GameObject.Find(tempPathName);
            if (tempPath == null)
            {
                Debug.Log("Failed to find path with that name");
            }
            MovementPath tempMove = tempPath.GetComponent<MovementPath>();

            CDpath = tempMove;
            //currentPath.MyPath = ABpath;
            //PickSprite(vehicleName);
            pickCursor++;
        } else
        {
            Debug.Log("Houston, we've had a problem!");
        }
    }

    public void QueryNextPath()
    {
        currentPath = playerVehicle.GetComponent<FollowPath>();
        if (currentPath.MyPath == ABpath)
        {
            currentPath.MyPath = BCpath;
            //Very memory-inefficient, in retrospect
            currentSprite.sprite = BCpath.linkedSprite;
            currentPath.Speed = BCpath.travelSpeed;
            //Purely so bike isn't so small, remove later
            //On second thought, no, it looks ugly
            //playerVehicle.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            currentPath.Start();
        } else if (currentPath.MyPath == BCpath)
        {
            currentPath.MyPath = CDpath;
            currentSprite.sprite = CDpath.linkedSprite;
            //currentPath.SetSpeed1();
            currentPath.Start();
        } else if (currentPath.MyPath == CDpath)
        {
            //Currently the last path
            currentPath.MyPath = CDpath;
            currentPath.Speed = 0;
            CancelInvoke("BurnFuel");
            WinScreen.SetActive(true);
            levelStarted = false;
        } else
        {
            Debug.Log("Warning: critical Failure");
        }
    }

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    //Deprecated, to be removed entirely
    /*
    public void PickSprite(string vehicleName)
    {
        if (vehicleName.ToLower() == "car")
        {
            currentSprite.sprite = carSprite;
        }
        if (vehicleName.ToLower() == "bike")
        {
            currentSprite.sprite = bikeSprite;
        }
        if (vehicleName.ToLower() == "boat")
        {
            currentSprite.sprite = boatSprite;
        }
        if (vehicleName.ToLower() == "scooter")
        {
            currentSprite.sprite = scooterSprite;
        }
    }*/

    public void BurnFuel()
    {
        if (currentSprite.sprite == carSprite)
        {
            PowerPoints -= 5;
            //Battery.value = PowerPoints;
        } else if (currentSprite.sprite == bikeSprite)
        {
            PowerPoints -= 3;
        } else if (currentSprite.sprite == boatSprite)
        {
            PowerPoints -= 4;
        }
        Battery.value = PowerPoints;
    }
}
