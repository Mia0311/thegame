using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterHouse : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject interactionPrompt; // Drag your UI prompt here in the Inspector
    public string sceneToLoad; 

    void Start()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false); // Ensure prompt is hidden at the start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionPrompt != null)
                interactionPrompt.SetActive(true); // Show prompt when player is near
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false); // Hide prompt when player leaves
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Hide the prompt before switching scenes
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
