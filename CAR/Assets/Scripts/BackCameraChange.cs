using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BackCameraChange : MonoBehaviour {

    [FormerlySerializedAs("BackCameras")] public GameObject[] backCameras;
    [FormerlySerializedAs("CameraChangeScript")] public CameraChange cameraChangeScript;

    [FormerlySerializedAs("CurrentBackCamera")] [HideInInspector] public int currentBackCamera;


    private void Start()
    {
        if (backCameras.Length == 0)
        { throw new Exception("No cameras attached to "+gameObject.name); }
    }

    private void Update () {

        if (Input.GetButtonDown("Backcam"))
        {
            Debug.Log("Back Cam int = " + currentBackCamera);
            currentBackCamera = cameraChangeScript.currentCamera;
            cameraChangeScript.cameras[cameraChangeScript.currentCamera].SetActive(false);
            if (currentBackCamera >= backCameras.Length)
            {
                currentBackCamera = 0;
                backCameras[currentBackCamera].SetActive(true);

                // Just in case I ever add more forward facing cameras than back facing cameras
                // This way there's no Array Out Of Bounds error
                // And while I could make a calculation instead of resetting it to the default 0,
                // I find it better that the back camera interacts slightly differently from the forward facing camera
            }
            else
            {
                backCameras[currentBackCamera].SetActive(true);
            }

            // I don't know if I should have done the camera rotation in the CameraChange script, but I felt it easier to do it in a separate place
            // I also didn't want to literally rotate it 180 degrees on the Y axis
            // Because kinda like the old GameBoy Need for Speed, it was literally the same angle (on the x axis) but rotated on its pivot
            // (which would be what the camera focuses on)
            // So this code gets which camera is currently active, and sets it as this one's active camera
        }
        
        if (Input.GetButtonUp("Backcam"))
        {
            backCameras[currentBackCamera].SetActive(false);
            cameraChangeScript.cameras[cameraChangeScript.currentCamera].SetActive(true);
        }
    }
}
