using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeController : MonoBehaviour {

    public Text timerText;
    private bool _startTimer;

    [HideInInspector] public static float TimerTime;
    [HideInInspector] public bool reset;

    public void Start()
    {
        if (timerText == null) { throw new Exception("Please set the time counter UI element in " + gameObject.name); }
        _startTimer = true;
        reset = true;
    }

    public void Update()
    {
        // I feel so incredibly dumb for how overly complicated I made the timer previously
        // How do I manage to make things that take up 5 lines, take up almost 50 lines
        // Anyways... TimerText is just part of the UI
        // float TimerTime is the currently displayed time, it only starts once the LapTimeController parent gets activated
        // convert TimerTime into an int, then divide by 60 with ToString as "00" so in the display it shows 00 from the start
        // To get an accurate second, without doing further stuff, you get the remainder which is a - (a/b) 
        // (in this case, TimerTime - (TimerTime/60))
        // Then just print that text, and milliseconds already get added behind the seconds
        // at start it's reset, or when LapComplete script says it should reset it also resets 
        // (public static is indeed dangerous, but a different script needs to access it and alter it sometimes)
        if (_startTimer == true)
        {
            TimerTime += Time.deltaTime;
            string minutes = ((int)TimerTime / 60).ToString("00");
            string seconds = (TimerTime % 60).ToString("f1");
            timerText.text = minutes + ":" + seconds;

            if (reset == true)
            {
                TimerTime = 0;
                reset = false;
            }
        }
    }
}