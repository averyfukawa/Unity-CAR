using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLapTime : MonoBehaviour {
    [Header("UI Best time GameObject")]
    public Text bestTimeCounter;
	
    // Here the best time is loaded up from the PlayerPrefs
    // As in LapTimeController, it only sets the best time in the player prefs

    private void Start () {
        if (bestTimeCounter == null)
        { throw new Exception("Please make sure the Best Time UI GameObject is attached to the " + gameObject.name); }

        float bestTime = PlayerPrefs.GetFloat("BestTime");
        string minutes = ((int)bestTime / 60).ToString("00");
        string seconds = (bestTime % 60).ToString("f1");

        bestTimeCounter.text = minutes + ":"+ seconds;
    }
}
