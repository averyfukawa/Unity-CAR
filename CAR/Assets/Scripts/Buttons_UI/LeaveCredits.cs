using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveCredits : MonoBehaviour
{
    [SerializeField] private float waitUntilLeave = 15.0f;
    
    public void Start()
    {
        StartCoroutine(nameof(LeaveCreditsScene));
    }

    IEnumerator LeaveCreditsScene()
    {
        yield return new WaitForSeconds(waitUntilLeave);
        SceneManager.LoadScene(0);
    }
}
