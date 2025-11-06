using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public float finalTime; // the value to carry between scenes

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep alive between scenes
        }
        else
        {
            Destroy(gameObject); // prevent duplicates
        }
    }
}
