using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_MiaStage2Controller : MonoBehaviour
{
    public void EditedOption()
    {
        Debug.Log("Edited Option button clicked!");
        // SceneManager.LoadScene("SampleScene"); 
    }

    public void NotEditedOption()
    {
        Debug.Log("Not Edited Option button clicked!");
        // SceneManager.LoadScene("SampleScene"); 
    }

    public void NotSureOption()
    {
        Debug.Log("Not Sure Option clicked!");
        // a
    }

    public void Setting()
    {
        Debug.Log("Setting clicked!");
        // a 
    }
    public void Tutorial()
    {
        Debug.Log("Tutorial clicked!");
        // a 
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }
}
