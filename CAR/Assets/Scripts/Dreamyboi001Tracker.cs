using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Dreamyboi001Tracker : MonoBehaviour {
    [FormerlySerializedAs("Markers")] public GameObject[] markers; 
    
    [FormerlySerializedAs("Marker")] public GameObject marker;
 
    [FormerlySerializedAs("MarkTracker")] public int markTracker;

    private bool _active = true;

    private void Start()
    {
        if (markers.Length==0) { Debug.Log("Dreamyboi001Tracker WARNING: empty marker list!"); return; }

        if (marker == null) { throw new Exception("Tracker is not attached to " + gameObject.name); }
        
    }

    public void OnTriggerEnter(Collider aiCollision)
    {
        if (!_active)
            return;
        if (aiCollision.gameObject.CompareTag("Dreamyboi01"))
        {
            _active = false;
            markTracker++;
            // Debug.Log("Dreamyboi01 is currently at: " + MarkTracker);

            if (markTracker >= markers.Length)
            {
                markTracker = 0;
            }
            if (markers[markTracker]==null)
            {
                Debug.Log("Dreamboi001Tracker WARNING: Markers not initialized!");

                markTracker = 0;
            }


            marker.transform.position = markers[markTracker].transform.position;
        }
    }


    public void OnTriggerExit(Collider aiCollision)
    {
        if (aiCollision.gameObject.CompareTag("Dreamyboi01"))
        {
            //Debug.Log("Exit");
            _active = true;
        }
    }
}
