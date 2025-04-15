using UnityEngine;
using UnityEngine.UI;

public class Script_HeartBarController : MonoBehaviour
{
    public Image heartBarImage;
    public Sprite[] heartBarStages;
    private int currentStage = 0;

    public void HeartIncrease()
    {
        if (currentStage < heartBarStages.Length - 1)
        {
            currentStage++;
            heartBarImage.sprite = heartBarStages[currentStage];
        }
        else
        {
            Debug.Log("You have collected enough heart to unlock a new item!");
        }
    }
}
