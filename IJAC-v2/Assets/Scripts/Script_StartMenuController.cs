using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject settingsMenuPrefab;
    public Transform canvas;
    private GameObject UIMenu;

    public void PlayGame1()
    {
        Debug.Log("Play 1 button clicked!");
        CloseSettingsMenu();
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
        if (UIMenu == null)
        {
            Debug.Log("Opening Settings...");
            UIMenu = Instantiate(settingsMenuPrefab);
            UIMenu.transform.SetParent(canvas, false);
            UIMenu.transform.SetAsLastSibling();
        }
        else
        {
            bool isActive = UIMenu.activeSelf;
            UIMenu.SetActive(!isActive);
            Debug.Log(isActive ? "Closing Settings..." : "Opening Settings...");
        }
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

    public void CloseSettingsMenu()
    {
        if (UIMenu != null)
        {
            Destroy(UIMenu);
            UIMenu = null;
        }
    }
}
