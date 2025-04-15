using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    public static SettingsMenuManager Instance;
    public GameObject settingsMenuPrefab;
    private GameObject settingsMenuInstance; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Keep this object across scenes

            if (settingsMenuPrefab != null)
            {
                //Instantiate the settings menu
                settingsMenuInstance = Instantiate(settingsMenuPrefab);
                settingsMenuInstance.SetActive(false); //Start inactive 
                DontDestroyOnLoad(settingsMenuInstance);
                Debug.Log("Settings Menu instantiated successfully.");
            }
            else
            {
                Debug.LogError("SettingsMenuPrefab is not assigned in the Inspector!");
            } 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Tab))
       {
            Debug.Log("TAB Key Pressed!"); // Debugging
            ToggleMenu(); 
       } 
    }

    public void ToggleMenu()
    {
        if (settingsMenuInstance == null)
        {
            Debug.LogError("SettingsMenuInstance is NULL! The menu might not be instantiated correctly.");
            return;
        }

        bool newState = !settingsMenuInstance.activeSelf;
        settingsMenuInstance.SetActive(newState);

        Debug.Log("Settings Menu is now: " + (newState ? "OPEN" : "CLOSED"));
    }

    /* Example Function to use when the appearance of the menu changes depending on which part of the game the player is at */
    /*public void UpdateMenuAppearance(string state)
    {
        switch (state)
        {
            case "combat":
                settingsMenuInstance.GetComponent<CanvasGroup>().alpha = 0.8f;
                break;
            case "explortion":
                settingsMenuInstance.GetComponent<CanvasGroup>().alpha = 1.0f;
                break;
            case "cutscene":
                settingsMenuInstance.SetActive(false);
                break;
        }
    }*/
}
