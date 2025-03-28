using UnityEngine;
using UnityEngine.UI;

public class Script_FlowerStatesController : MonoBehaviour
{
    public Image flowerPotImage;    
    public Sprite[] flowerStages;   
    private int currentStage = 0;   

    public void GrowFlower()
    {
        if (currentStage < flowerStages.Length - 1)
        {
            currentStage++;
            flowerPotImage.sprite = flowerStages[currentStage];
        }
        else
        {
            Debug.Log("Flower has fully grown!");
        }
    }
}
