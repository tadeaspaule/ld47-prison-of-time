using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Application.Quit();
        else if (Input.GetKeyDown(KeyCode.E)) SceneManager.LoadScene("MenuScene");
    }
}
