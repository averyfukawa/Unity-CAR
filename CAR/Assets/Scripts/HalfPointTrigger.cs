using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HalfPointTrigger : MonoBehaviour {

    public GameObject lapCompleteTrig;
    public GameObject halfLapTrig;

    private void Start()
    {
        if (lapCompleteTrig == null || halfLapTrig == null)
        {
            throw new Exception("Please attach the half or lap complete trigger in " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider target) {
        if (target.CompareTag("CarPosition") || target.CompareTag("Dreamyboi01"))
        {
            lapCompleteTrig.SetActive(true);
            halfLapTrig.SetActive(false);
        }
    }
}
