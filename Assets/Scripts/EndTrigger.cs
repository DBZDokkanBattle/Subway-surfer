using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private string nextScene = "EndingScene";
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

        Debug.Log($"[EndTrigger] Switching scenes. Time saved = {GameData.Instance.finalTime:0.00}s");
        SceneManager.LoadScene(nextScene);
    }
}