using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void PlayGame1()
    {
        Debug.Log("Play 1 button clicked!");
        // SceneManager.LoadScene("SampleScene"); 
    }

    public void PlayGame2()
    {
        Debug.Log("Play 2 button clicked!");
        // SceneManager.LoadScene("SampleScene"); 
    }

    public void ChangeAvatar()
    {
        Debug.Log("Change Avatar clicked!");
        // Implement avatar changing functionality
    }

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
}
