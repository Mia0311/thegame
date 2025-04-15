using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_IntroductionMenuController : MonoBehaviour
{
    public void Setting()
    {
        Debug.Log("Setting clicked!");
        // Implement setting functionality
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
