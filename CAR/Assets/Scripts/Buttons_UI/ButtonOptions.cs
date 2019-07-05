using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonOptions : MonoBehaviour
{
    // I genuinely don't know if buttons can be done more efficiently
    // But this works
    // Only "downside" is if I add a new scene between existing scenes, I need to edit it in here
    // But this is the only heavy scene management button script
    // Because the one placed in the win/lose UI splashscreens are easily updated per scene
    public Animator fadeAnimator;
    public Image fadeImage;
    public GameObject fadeImageObject;

    private void Start()
    {
        if (fadeAnimator == null || fadeImage == null || fadeImageObject == null)
        {
            throw new Exception("Null reference in ButtonOptions component attached to "+gameObject.name);
        }
        fadeImageObject.SetActive(true);
    }

    private IEnumerator Fade(int level)
    {
        fadeAnimator.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(()=> fadeImage.color.a == 1);
        SceneManager.LoadScene(level);
    }
    
    public void PlayGame()
    {
        StartCoroutine(Fade(1));
    }

    public void Tutorial()
    {
        StartCoroutine(Fade(6));
    }

    public void CreditsScreen()
    {
        StartCoroutine(Fade(5));
    }

    public void MainMenu()
    {
        StartCoroutine(Fade(0));
    }
    
    public void QuitGame()
    {
        Debug.Log("GAME QUIT");
        Application.Quit();
    }

    // Track selection buttons here
    public void Track_01()
    {
        StartCoroutine(Fade(2));
    }

    public void Track_02()
    {
        StartCoroutine(Fade(3));
    }

    public void Track_03()
    {
        StartCoroutine(Fade(4));
    }
}