using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class PlayerVelocity : MonoBehaviour
{
    private float _kph;
    public GameObject playerCar;
    public Text kphDisplay;

    private void Start()
    {
        if (playerCar == null) { throw new Exception("Please set the PlayerCar to " + gameObject.name); }
        if (kphDisplay == null) { throw new Exception("Please set the speed UI display in " + gameObject.name); }
    }

    private void Update()
    {
        // kph = PlayerCar.GetComponent<CarController>().CurrentSpeed;
        _kph = playerCar.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        // velocity returns the speed in meters per second
        // simple math of 3600 m/h, which is 3.6 km/h
        _kph = Mathf.RoundToInt(_kph);
        kphDisplay.text = _kph + " KPH";
    }
}
