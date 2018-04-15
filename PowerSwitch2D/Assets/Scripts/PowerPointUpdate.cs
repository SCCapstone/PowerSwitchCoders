﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPointUpdate : MonoBehaviour {

    [Header("Power Point Panel Objects")]
    [Tooltip("color")]
    public PathHandler pathHandler;
    public Slider pSlider;
    public Text pPoints;
    public Image pIcon;
    public Image pFill;

    [Header("Preset Colors - Green, Yellow, Red")]
    [Tooltip("Colors for PowerPoints as they decrease")]
    public Color32 Cgreen = new Color32(0,255,0,255);
    public Color32 Cyellow = new Color32(255,255,0,255);
    public Color32 Cred = new Color32(255, 0, 0, 255);

    //private float nextUpdate = 0.5f;
    private int powerPoints;
    private int count = 0;
    private float pathCost;
    private float pathTime;
    private float endVal;
    private bool textUpdate = false;
    //https://answers.unity.com/questions/122349/how-to-run-update-every-second.html    
    //do above


    //GIZMOS
    public void OnDrawGizmos()
    {
        if (!(pathHandler == null))
        {
            Gizmos.DrawLine(pathHandler.transform.position, this.transform.position);
        }
        else
        {
            return;
        }
    }

    // Use this for initialization
    void Start () {
        powerPoints = pathHandler.powerPoints;
        pSlider.value = pSlider.maxValue = pathHandler.powerPoints;
        pPoints.text = (Mathf.Floor(100 * (pSlider.value / pSlider.maxValue))).ToString();
        Debug.Log(100 * (pSlider.value / pSlider.maxValue));
	}
	
	// Update is called once per frame
	void Update () {
		//Update actual Power Points text and Panel item colors 
        if (textUpdate)
        {
            pPoints.text = (Mathf.Floor(100 * (pSlider.value / pSlider.maxValue))).ToString();
            if (count == 0 && pSlider.value <= (pSlider.maxValue / 2))
            {
                pFill.color = Cyellow;
                pIcon.color = Cyellow;
                pPoints.color = Cyellow;
                count = 1;
            }
            if (count == 1 && pSlider.value <= (pSlider.maxValue / 4))
            {
                pFill.color = Cred;
                pIcon.color = Cred;
                pPoints.color = Cred;
                count = 2;
            }
            if (count == 2 && pSlider.value <= endVal)
            {
                StopCoroutine(AnimateSliderOverTime(15.0f));
                pIcon.GetComponentInParent<Spinner>().rotationSpeed = 0;
                Time.timeScale = 0.0f;
                count = 3;
            }
        }

	}

    public void doUpdate()
    {
        //Do check for failiing or winning path here -  if failing, run out of power 5 seconds before pathtime
        //If winning, don't 
        pathCost = pathHandler.pickedPathCost;
        pathTime = pathHandler.GetTravelTime();
        endVal = powerPoints - pathCost;
        if (endVal <= 0.0f)
        {
            endVal = 0.0f;
        }
        bool willWin = pathHandler.CheckWinningPath();
        //15 seconds for now
        //StartCoroutine(AnimateSliderOverTime(15.0f));
        if (willWin)
        {
            StartCoroutine(AnimateSliderOverTime(pathTime));
        } else
        {
            StartCoroutine(AnimateSliderOverTime(pathTime-3.0f));
        }
        
        textUpdate = true;
    }

    public void UpdatePanel()
    {
        powerPoints = pathHandler.powerPoints;
        pSlider.value = powerPoints;
        pPoints.text = (Mathf.Floor(100 * (pSlider.value / pSlider.maxValue))).ToString();

        //PowerPoints below 50%
        if (count == 0 && powerPoints <= (pSlider.maxValue/2))
        {
            pFill.color = Cyellow;
            pIcon.color = Cyellow;
            pPoints.color = Cyellow;
            count = 1;
        }
        //PowerPoints below 25%
        if (count == 1 && powerPoints <= (pSlider.maxValue / 4))
        {
            pFill.color = Cred;
            pIcon.color = Cred;
            pPoints.color = Cred;
            count = 2;
        }
    }

    //Animate slider over certain amount of time 
    IEnumerator AnimateSliderOverTime(float seconds)
    {
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            pSlider.value = Mathf.Lerp(powerPoints, endVal, lerpValue);
            yield return null;
        }
    }
}
