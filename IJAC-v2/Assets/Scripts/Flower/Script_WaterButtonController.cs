using UnityEngine;

public class Script_WaterButtonController : MonoBehaviour
{
    public Animator waterCanAnimator;
    public Script_FlowerStatesController flowerGrowthController;

    public void TriggerWateringAnimation()
    {
        waterCanAnimator.SetTrigger("Water");
        Invoke("GrowFlowerState", 3f);
    }

    void GrowFlowerState()
    {
        flowerGrowthController.GrowFlower();
    }
}
