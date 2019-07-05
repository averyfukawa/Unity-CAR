using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class RaceFinished : MonoBehaviour {

    [Header("Vehicles and position")]
    public GameObject playerCar;
    public GameObject aiCar;
    public PositionUp positionChecker;

    [Header("GameObjects that get disabled")]
    public GameObject playerUi;
    public GameObject viewPort;
    public GameObject levelMusic;
    public GameObject completeTrigger;
    
    [Header("Race Finish GameObjects")]
    public GameObject finishedCamera;
    public AudioSource finishedMusic;

    public LapComplete lapScript;
    [Space]
    public GameObject raceWinUISplashScreen;
    public GameObject raceLoseUISplashScreen;
    
    private float _timeForSplashScreen;
    
    private void Start()
    {
        _timeForSplashScreen = finishedMusic.clip.length/2; // I don't want the splash screen to appear while the music is still playing
        
        if (playerCar == null || aiCar == null)
        { throw new Exception("The cars or PositionUp script have not been placed in "+gameObject.name); }

        if (playerUi == null || viewPort == null || levelMusic == null || completeTrigger == null)
        { throw new Exception("The PlayerUI, ViewPort, LevelMusic, or CompleteTrigger have not been placed in "+gameObject.name); }

        if (finishedCamera == null || finishedMusic == null)
        { throw new Exception("The finished camera or finished music has not been placed in " + gameObject.name); }

        if (raceWinUISplashScreen == null || raceLoseUISplashScreen == null)
        { throw new Exception("The UI SplashScreens have not been placed in " + gameObject.name); }
    }

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<BoxCollider>().enabled = false;

        // So this doesn't mess up with the car valuables
        completeTrigger.SetActive(false);

        CarController.m_Topspeed = 0.0f; // Stop its movement

        // Reactivated so everything else that should be happening, and the camera + rotation.
        finishedCamera.SetActive(true);
        levelMusic.SetActive(false); // Stop playing the background track, so the finished track plays now
        viewPort.SetActive(false);
        finishedMusic.Play();
        
        // Stop the player from changing view with C, so now only the new camera plays
        
        /*
        This is a haiku
        A kinda bad code haiku
        End my torment please
        */

        // Now begins the things that were causing errors. Sound script is codependent with the Cars' Controller script
        // Meaning they need to individually be turned off so the error doesn't bog down important errors
        // So the little stop sound class runs
        playerCar.GetComponent<CarAudio>().StopSound();
        playerCar.GetComponent<CarAudio>().enabled = false;

        // Same for the other car(s)
        aiCar.GetComponent<CarAudio>().StopSound();
        aiCar.GetComponent<CarAudio>().enabled = false;
        
        playerUi.SetActive(false);

        // Start the small timer for whether the Win interface or lose interface needs to come up
        if (lapScript.aiWin == true)
            StartCoroutine(nameof(LoseSplash));

        if (lapScript.playerWin == true)
            StartCoroutine(nameof(WinSplash));
    }

    private IEnumerator WinSplash()
    {
        yield return new WaitForSeconds(_timeForSplashScreen);
        
        raceWinUISplashScreen.SetActive(true);
    }

    private IEnumerator LoseSplash()
    {
        yield return new WaitForSeconds(_timeForSplashScreen);

        raceLoseUISplashScreen.SetActive(true);
    }
}