using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerToLeaderboard : MonoBehaviour
{
    [Header("Optional UI")]
    public TextMeshProUGUI timerText;

    private float elapsedTime;
    private bool isRunning = true;

    private void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;
        if (timerText)
        {
            int m = Mathf.FloorToInt(elapsedTime / 60f);
            float s = elapsedTime % 60f;
            timerText.text = $"{m:00}:{s:00.00}";
        }
    }

    public void StopAndSave()
    {
        isRunning = false;
        if (GameData.Instance != null)
        {
            GameData.Instance.finalTime = elapsedTime;
            Debug.Log($"[TimerToLeaderboard] Saved time = {elapsedTime:0.00}");
        }
    }

    public void GoToLeaderboard()
    {
        StopAndSave();
        SceneManager.LoadScene("LeaderBoard");
    }

    // Call this only in the LeaderBoard scene
    public void LoadAndDisplayFinalTime()
    {
        if (GameData.Instance != null && timerText)
        {
            float t = GameData.Instance.finalTime;
            int m = Mathf.FloorToInt(t / 60f);
            float s = t % 60f;
            timerText.text = $"Final Time: {m:00}:{s:00.00}";
            Debug.Log($"[TimerToLeaderboard] Loaded final time = {t:0.00}");
        }
    }
}
