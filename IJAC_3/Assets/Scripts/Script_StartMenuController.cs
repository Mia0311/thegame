using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject settingsMenu;
    
    void Start()
    {
        settingsMenu.SetActive(false);
    }

    public void PlayGame1()
    {
        Debug.Log("Play 1 button clicked!");
        SceneManager.LoadScene("MapOutDoor"); 
    }

    /*public void PlayGame2()
    {
        Debug.Log("Play 2 button clicked!");
        SceneManager.LoadScene("MapOutDoor"); 
    }*/

    /*public void ChangeAvatar()
    {
        Debug.Log("Change Avatar clicked!");
        // Implement avatar changing functionality
    }*/

    public void Setting()
    {
        Debug.Log("Setting clicked!");
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        //For testing purposes, check if we are in the Unity Editor or not 
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
