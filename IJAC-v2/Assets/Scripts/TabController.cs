using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [Header("Tabs & Pages")]
    public Button[] tabButtons;
    public Image[] tabImages;
    public GameObject[] pages;

    [Header("Colors")]
    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;

    void Start()
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int tabIndex = i; // Important to avoid closure issue
            tabButtons[i].onClick.AddListener(() => ActivateTab(tabIndex));
        }

        ActivateTab(0); // Initial tab
    }

    public void ActivateTab(int tabIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            bool isActive = (i == tabIndex);
            pages[i].SetActive(isActive);
            tabImages[i].color = isActive ? activeColor : inactiveColor;
        }
    }
}
