using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCar : MonoBehaviour
{
    public static int CarType;
    // Again, I don't know how one would make buttons be part of an array
    // but this works and can be further expanded at will, as the actual body choice is part of an array
    // public static because we want the CarType to remain while the player does consecutive laps

    public GameObject trackWindow;

    private void Start()
    {
        if (trackWindow == null) { throw new Exception("Please attach the TrackDisplay to "  + gameObject.name); }
    }

    public void PinkCar()
    {
        CarType = 1;
        trackWindow.SetActive(true);
    }

    public void BrownCar()
    {
        CarType = 2;
        trackWindow.SetActive(true);
    }
}
