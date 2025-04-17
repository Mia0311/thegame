using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_IntroductionMenuController : MonoBehaviour
{
    public GameObject settingsMenuPrefab;
    public Transform canvas;
    private GameObject UIMenu; 
   
    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    public void LoadMenuScene()
    {
        Debug.Log("Start button clicked!");
        CloseSettingsMenu();
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadSettingMenu ()
    {
        Debug.Log("Settings button clicked!");
        
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

    public void CloseSettingsMenu()
    {
        if (UIMenu != null)
        {
            Destroy(UIMenu);
            UIMenu = null;
        }
    }

}
