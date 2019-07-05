using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour {
    [Header("Trigger Game Objects")]
    
    public GameObject halfLapTrigger; // Trigger object of the lap's halfway point is added to this
    public GameObject raceFinished; // Trigger object of the lap's finished trigger
    public LapTimeController lapTimeControllerScript;
    public PositionUp positionChecker;

    [Header("UI Game Objects")]
    public Text bestTimer;
    public Text lapCounter;
    
    [Space]
    [SerializeField] private int maxLaps = 3; // Default 3 laps per level, but it's a serialized field for ease of changing the quanity

    [HideInInspector] public int playerLaps; // How many laps has the player done?
    [HideInInspector] public int aiLaps; // How many laps has the AI done?
    [HideInInspector] public bool aiWin = false;
    [HideInInspector] public bool playerWin = false;
    private bool _hasBeenCrossed = false;

    private void Start()
    {
        if (halfLapTrigger == null || raceFinished == null)
        { throw new Exception("Please attach the World Triggers to " + gameObject.name); }
        if (bestTimer == null || lapCounter == null) { throw new Exception("Please attach the UI Element to "  + gameObject.name); }
        if (lapTimeControllerScript == null) { throw new Exception("Please attach the LapTimeController to " + gameObject.name); }
        if (positionChecker == null) { throw new Exception("Please attach the PositionChecker to " + gameObject.name); }

        bestTimer.text = "" + PlayerPrefs.GetFloat("BestTime"); ;
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player") && halfLapTrigger.activeInHierarchy == true && _hasBeenCrossed == false)
        {
            playerLaps++;
            _hasBeenCrossed = true;
        }

        if (target.gameObject.CompareTag("Dreamyboi01") && halfLapTrigger.activeInHierarchy == true && _hasBeenCrossed == false)
        {
            aiLaps++;
            _hasBeenCrossed = true;
        }

        if (playerLaps == maxLaps)
        {
            if (positionChecker.firstPlace == true)
            {
                playerWin = true;
                raceFinished.SetActive(true);
            }
            else
            {
                aiWin = true;
            }
        }

        if (aiLaps == maxLaps)
        {
            aiWin = true;
            raceFinished.SetActive(true);
        }
        
        lapCounter.GetComponent<Text>().text = "" + playerLaps + "/" + maxLaps;

        halfLapTrigger.SetActive(true);
        // Self Explanatory

        Debug.Log("Current player laps " + playerLaps + " Current AI laps " + aiLaps);
        Debug.Log("AIWin bool =  " + aiWin + " PlayerWin = " + playerWin);
        Debug.Log("Tag crossing the LapCompleteTrigger " + target.tag);

        // This was reduced from like a bunch of if statements to this
        // Check the PlayerPrefs, then do the time calculations, then you display it in the same manner as in the other time text boxes
        if (LapTimeController.TimerTime <= PlayerPrefs.GetFloat("BestTime") || (PlayerPrefs.GetFloat("BestTime") <= 0.0f))
        {
            string minutes = ((int)LapTimeController.TimerTime / 60).ToString("00");
            string seconds = (LapTimeController.TimerTime % 60).ToString("f1");
            bestTimer.text = minutes + ":" + seconds;
            // BestTimer.text = LapTimeController.timerTime.ToString();
            PlayerPrefs.SetFloat("BestTime", LapTimeController.TimerTime);
        }

        lapTimeControllerScript.reset = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _hasBeenCrossed = false;
    }
}
