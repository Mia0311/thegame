using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonsManager : MonoBehaviour
{
    public GameObject settingsMenuPrefab;
    public Transform canvas;
    private GameObject UIMenu;

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
}
