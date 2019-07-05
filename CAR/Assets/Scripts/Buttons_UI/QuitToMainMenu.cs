using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") == true) {
            SceneManager.LoadScene(0);
        }
    }
}
