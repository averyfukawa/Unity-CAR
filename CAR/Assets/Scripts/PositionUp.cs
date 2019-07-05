using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class PositionUp : MonoBehaviour
{
    public GameObject positionDisplay;

    public GameObject player;
    public GameObject dreamyboi01;

    [HideInInspector] public bool firstPlace = true;

    private void Start()
    {
        if (positionDisplay == null) { throw new Exception("Please insert the UI Position Counter to " + gameObject.name); }
        if (player == null || dreamyboi01 == null) { throw new Exception("Please attach the cars to " + gameObject.name); }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CarPosition"))
        {
            if (dreamyboi01.GetComponent<CarController>().CurrentSpeed > player.GetComponent<CarController>().CurrentSpeed)
            {
                positionDisplay.GetComponent<Text>().text = "2nd Place";
                firstPlace = false;
            }
            else
            {
                positionDisplay.GetComponent<Text>().text = "1st Place";
                firstPlace = true;
            }
                
        }

    }
}
