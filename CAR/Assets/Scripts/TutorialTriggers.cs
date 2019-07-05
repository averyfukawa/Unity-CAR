using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    public GameObject currentCheckpoint;
    public GameObject nextCheckpoint;

    private void Start()
    {
        if (currentCheckpoint == null || nextCheckpoint == null) 
        { throw new Exception("Missing Tutorial triggers attached to "+gameObject.name); }
    }

    private void OnTriggerEnter(Collider target)
    {
        currentCheckpoint.SetActive(false);
        nextCheckpoint.SetActive(true);
    }
}
