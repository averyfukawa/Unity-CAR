using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
// using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof (CarManager))] // Import the relevant car here
public class CarUserInput : MonoBehaviour
{
    private CarManager m_Car; // the car controller we want to use


    private void Awake()
    {
        if (!GetComponent<CarManager>())
        {
            Debug.Log("Please connect the car in the inspector!");
        }

        // get the car controller
        m_Car = GetComponent<CarManager>();
    }


    private void FixedUpdate()
    {
        // pass the input to the car!
        // I didn't check out the CrossPlatformInputManager, but the logic remains the same 
        //it gets the Unity Inputs, which are the default keycodes in the project settings, which can have more or less added
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        // #if ending on #endif, is for it to act on a boolean (in this case, mobile input is false) 
        // >>only<< when it's true. 
        // The handbrake, being the "Jump" axis, is disabled on mobile.
#if !MOBILE_INPUT
        float handbrake = CrossPlatformInputManager.GetAxis("Jump");
        m_Car.Move(h, v, v, handbrake);
#else
        m_Car.Move(h, v, v, 0f);
#endif
    }
}