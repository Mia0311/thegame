using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[Serializable]
public class PlayerData
{
    public string username;
    public string age;
    public string gender;
    public string screenTime;
    public bool hasEnteredHouseBefore;
}

public class PlayerDataManager : MonoBehaviour
{
    public GameObject playerInfoUI;
    public TMP_InputField nameInput;
    public TMP_Dropdown ageDropdown;
    public TMP_Dropdown genderDropdown;
    public TMP_Dropdown screenTimeDropdown;
    public Button saveButton;
    public TMP_Text warningText;

    private string saveFilePath;
    private PlayerData playerData;

    private PlayerMovement playerMovement;

    void Start ()
    {
        saveFilePath = Application.persistentDataPath + "/playerData.json";
        Debug.Log("Save path: " + Application.persistentDataPath); //To know where the file is located

        //Find Player 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null )
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);

            if (playerData.hasEnteredHouseBefore)
            {
                playerInfoUI.SetActive(false);
                return;
            }
        }
        else
        {
            playerData = new PlayerData();
        }

        //Show UI for first time input
        playerInfoUI.SetActive(true);

        if (playerMovement != null)
        {
            playerMovement.canMove = false;
        }

        saveButton.onClick.AddListener(SavePlayerData);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R)) //Ctrl + R
        {
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
                Debug.Log("Save file deleted.");
            }
            else
            {
                Debug.Log("No save file found to delete.");
            }
        }
    }

    void SavePlayerData ()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            warningText.text = "Please fill in all required fields.";
            return;
        }

        playerData.username = nameInput.text;
        playerData.age = ageDropdown.options[ageDropdown.value].text;
        playerData.gender = genderDropdown.options[genderDropdown.value].text;
        playerData.screenTime = screenTimeDropdown.options[screenTimeDropdown.value].text;
        playerData.hasEnteredHouseBefore = true;

        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(saveFilePath, json);

        playerInfoUI.SetActive(false); //Close the popup

        if (playerMovement != null)
        {
            playerMovement.canMove = true;
        }
    }
}