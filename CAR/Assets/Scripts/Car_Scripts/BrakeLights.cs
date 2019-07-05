using System;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class BrakeLights : MonoBehaviour
{
    public CarManager CarManagerScript; // reference to the car controller, must be dragged in inspector

    private Renderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }


    private void Update()
    {
        // enable the Renderer when the car is braking, disable it otherwise.
        m_Renderer.enabled = CarManagerScript.BrakeInput > 0f;
    }
}
