using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPointUpdate : MonoBehaviour {

    [Header("Power Point Panel Objects")]
    [Tooltip("color")]
    public TheMagicAlgorithm PathHandler;
    public Slider pSlider;
    public Text pPoints;
    public Image pImage;

    [Header("Preset Colors - Green, Yellow, Red")]
    [Tooltip("Colors for PowerPoints as they decrease")]
    public Color32 green = new Color32(0,255,0,255);
    public Color32 yellow = new Color32(255,255,0,255);
    public Color32 red = new Color32(255, 0, 0, 255);

	// Use this for initialization
	void Start () {
        pSlider.value = pSlider.maxValue = PathHandler.powerPoints;
        pPoints.text = (Mathf.Floor(pSlider.value / pSlider.maxValue)).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
