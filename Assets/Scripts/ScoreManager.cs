using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int score = 0;
    public AudioSource audioSource;
    public AudioClip collectSound; 

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }
}