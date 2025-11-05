using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    
    [SerializeField] private string endingSceneName = "EndingScene"; 

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player")) 
        {
            hasTriggered = true;
            SceneManager.LoadScene(endingSceneName);
        }
    }

    
}