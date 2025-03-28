using UnityEngine;

public class Script_TestingButtonController : MonoBehaviour
{
    public Animator flyingHeartAnimator;
    public Script_HeartBarController heartIncreaseController;

    public float heartBarUpdateDelay = 2f;

    public void TriggerHeartFlyAnimation()
    {
        flyingHeartAnimator.SetTrigger("FlyHeart");
        Invoke(nameof(NextHeartBarState), heartBarUpdateDelay);
    }

    void NextHeartBarState()
    {
        heartIncreaseController.HeartIncrease();
    }
}
