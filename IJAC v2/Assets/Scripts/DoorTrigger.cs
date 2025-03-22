using UnityEngine;
using UnityEngine.SceneManagement; 

public class DoorTrigger : MonoBehaviour
{
    private bool playerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            GameManager.isReturningFromIndoor = true;
            SceneManager.LoadScene("MainHouseIndoor");
        }
    }
}
