using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.Vehicles.Car;

public class CarControlActive : MonoBehaviour {
    public GameObject playerCar;
    public GameObject dreamyboi01;

    private void Start()
    {
        playerCar.GetComponent<CarUserControl>().enabled = true;
        dreamyboi01.GetComponent<CarAIControl>().enabled = true;

        if (playerCar == null || dreamyboi01 == null)
        { throw new Exception("Please attach the vehicles to " + gameObject.name); }
    }

}