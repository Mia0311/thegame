using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_PlayerPageController : MonoBehaviour
{
    [Header("Dropdown References")]
    public TMP_Dropdown ageDropdown;
    public TMP_Dropdown genderDropdown;
    public TMP_Dropdown screentimeDropdown;

    [Header("UI Elements")]
    public Button saveButton;
    public TMP_Text warningText;

    void Start()
    {
        warningText.text = ""; // hide warning
        saveButton.onClick.AddListener(OnSaveClicked);
    }
    void OnSaveClicked()
    {
        if (ageDropdown.value == 0 || genderDropdown.value == 0 || screentimeDropdown.value == 0)
        {
            // show warning message
            warningText.text = "Please fill in all the information before saving!";
        }
        else
        {
            // clear warning
            warningText.text = "";

            // Successfully save player info
            string age = ageDropdown.options[ageDropdown.value].text;
            string gender = genderDropdown.options[genderDropdown.value].text;
            string screentime = screentimeDropdown.options[screentimeDropdown.value].text;

            Debug.Log($"Saved Player Info: Age: {age}, Gender: {gender}, Screen Time: {screentime}");

        }
    }
}

