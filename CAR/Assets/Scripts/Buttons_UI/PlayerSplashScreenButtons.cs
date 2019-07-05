using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSplashScreenButtons : MonoBehaviour
{
    [SerializeField] private int nextSceneNumber = 0;
    [SerializeField] private int currentScene = 0;

    [Header("Scene Fade GameObject")]
    public Animator fadeAnimator;
    public Image fadeImage;
    public GameObject fadeImageObject;

    IEnumerator Fade(int level)
    {
        fadeAnimator.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(()=> fadeImage.color.a == 1);
        SceneManager.LoadScene(level);
    }
    
    private void Start()
    {
        if (fadeAnimator == null || fadeImage == null || fadeImageObject == null) 
        { throw new Exception("Please set the cars on the " + gameObject.name); }

        fadeImageObject.SetActive(true);
    }

    public void WinScreenNextTrackButton()
    {
        StartCoroutine(Fade(nextSceneNumber));
    }

    public void RestartCurrentTrackButton()
    {
        StartCoroutine(Fade(currentScene));
    }

    public void MainMenuButton()
    {
        StartCoroutine(Fade(0));
    }
}
