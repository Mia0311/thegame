using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_MiaStage3Controller : MonoBehaviour
{
    public void UpvoteButton()
    {
        Debug.Log("Upvote button clicked!");
        // SceneManager.LoadScene("SampleScene"); 
    }

    public void DownvoteButton()
    {
        Debug.Log("Downvote button clicked!");
        // SceneManager.LoadScene("SampleScene"); 
    }

    public void ReportButton()
    {
        Debug.Log("Report clicked!");
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
