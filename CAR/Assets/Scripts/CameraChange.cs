using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraChange : MonoBehaviour {

    public GameObject[] cameras;

    [HideInInspector] public int currentCamera;

    private void Start() {
        if (cameras.Length == 0)
        { Debug.Log("Please attach the forward facing cameras!"); }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Viewport"))
        {
            cameras[currentCamera].SetActive(false);

            currentCamera++;

            // Check the length of the array in the inspector, if longer than it, reset to 0, remember kiddos - arrays start at 0.
            // Arrays fixed my life, look at how little it can be!
            if (currentCamera >= cameras.Length)
            {
                currentCamera = 0;
            }
            cameras[currentCamera].SetActive(true);
        }
    }
}
