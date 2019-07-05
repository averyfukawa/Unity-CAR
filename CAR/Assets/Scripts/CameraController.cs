using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private float _carX;
    private float _carY;
    private float _carZ;

    // I don't fully understand how eulerAngles work
    // but the solutions for a steady and smooth camera online recommended this
    // What I do understand is the y axis rotation doesn't get changed, that way the camera (what it is focusing on) is always 0
    private void Start()
    {
        if (player == null) { throw new Exception("Please attach the player to " + gameObject.name); }
    }

    private void Update()
    {
        var eulerAngles = player.transform.eulerAngles;
        _carX = eulerAngles.x;
        _carY = eulerAngles.y;
        _carZ = eulerAngles.z;

        transform.eulerAngles = new Vector3(_carX - _carX, _carY, _carZ - _carZ);
    }
}
