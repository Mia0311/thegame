using UnityEngine;
using UnityEngine.UI;

public class Script_HeartAnimationController : MonoBehaviour
{
    private Image heartImage; 
    void Start()
    {
        heartImage = GetComponent<Image>();
        heartImage.enabled = false; 
    }

    public void PlayHeartAnimation()
    {
        heartImage.enabled = true;  
        GetComponent<Animator>().Play("heart_rotation", -1, 0f); 
    }

    public void HideHeart()
    {
        heartImage.enabled = false; 
    }
}
