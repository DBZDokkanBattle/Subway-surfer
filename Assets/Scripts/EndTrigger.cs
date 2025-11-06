using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;

        Timer timer = FindFirstObjectByType<Timer>();
        if (timer != null)
        {
            timer.StopTimer(); // Save time to GameData
        }

        
        SceneManager.LoadScene("EndingScene");
    }
}