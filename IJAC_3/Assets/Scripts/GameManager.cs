using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Vector3 playerPos;
    public static bool isReturningFromIndoor = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Keeps the GameManager across scenes 
        }
        else
        {
            Destroy(gameObject); //Prevent duplicates
        }
    }
}
