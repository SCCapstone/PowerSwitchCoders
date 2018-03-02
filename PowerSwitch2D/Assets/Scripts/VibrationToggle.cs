using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationToggle : MonoBehaviour
{

    Toggle toggleBox;

    // Use this for initialization
    void Start()
    {

        //Grabs the toggle component for reference.
        toggleBox = GetComponent<Toggle>();


        if (PlayerPrefs.HasKey("vibration"))
        {
            toggleBox.isOn = getBool(PlayerPrefs.GetInt("vibration"));
        }
        else
        {
            PlayerPrefs.SetInt("vibration", 1);
            toggleBox.isOn = true;
        }

    }

    //Allows the change of the PlayerPrefs saved value from the 'On Value Changed' event handler of the attached toggle box.
    //This method is called whenever the toggle box value is changed.
    public void ChangeSetting()
    {
        if (toggleBox.isOn == true)
            PlayerPrefs.SetInt("vibration", 1);
        else
            PlayerPrefs.SetInt("vibration", 0);

    }

    //'PlayerPrefs' doesn't store boolean values, and I was having an unecessary amount of trouble converting an int stored
    //in 'PlayerPrefs' to a boolean...
    bool getBool(int anInt)
    {
        if (anInt == 1)
            return true;
        else
            return false;
    }

}
