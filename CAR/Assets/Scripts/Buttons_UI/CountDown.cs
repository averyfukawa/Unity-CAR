using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    [Header("What plays while the countdown is going")]
    public GameObject countdown;
    public AudioSource getReadyAudioSource;
    public AudioSource goAudioSource;

    [Header("What gets activated after the countdown")]
    public GameObject levelMusic;
    public GameObject lapTimer;
    public GameObject carControls;

    // Probably inefficient way to check for second... 
    // But the animation tab doesn't record changes to text, so I did it manually through code
	void Start () {
        if (countdown == null || getReadyAudioSource == null || goAudioSource == null)
        { throw new Exception("Please attach the correct SFX or UI CountDown to " + gameObject.name);}
        if (levelMusic == null || lapTimer == null || carControls == null)
        { throw new Exception("Please attach the correct GameObjects to " + gameObject.name); }
        StartCoroutine(CountStart());
	}

    IEnumerator CountStart()
    {
        // Wait a little bit before starting the start countdown
        yield return new WaitForSeconds(0.5f);
        countdown.GetComponent<Text>().text = "3"; // Just in case the UI doesn't say 3
        getReadyAudioSource.Play();
        countdown.SetActive(true);

        // Let the animation play for a second, then change it to 2
        yield return new WaitForSeconds(1);
        countdown.SetActive(false); // Deactivate it to start again
        countdown.GetComponent<Text>().text = "2";
        getReadyAudioSource.Play();
        countdown.SetActive(true);

        // Same, then change to 1
        yield return new WaitForSeconds(1);
        countdown.SetActive(false); // Deactivate it for the next number
        countdown.GetComponent<Text>().text = "1";
        getReadyAudioSource.Play();
        countdown.SetActive(true);

        // Zeros need some love too so the zero turns into a hero jk it actually never shows up
        yield return new WaitForSeconds(1);
        countdown.SetActive(false); // Deactivate it again ): poor thing can't catch a break

        // Well? The player needs to hear they have to gogoGO
        goAudioSource.Play();
        levelMusic.SetActive(true); // BKG Music initialized after the beeping stops

        lapTimer.SetActive(true); // /Now/ it can start timing the player
        carControls.SetActive(true); // And give them their controls, of course
    }
}
