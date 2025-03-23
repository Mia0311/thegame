using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Vector3 playerPos; 
    public static bool isReturningFromIndoor = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); 
    }
}
