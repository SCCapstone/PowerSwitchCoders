using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefReset : MonoBehaviour {
    public bool doReset = false;

	// Use this for initialization
	void Start () {
        if (doReset)
        {
            PlayerPrefs.DeleteAll();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
