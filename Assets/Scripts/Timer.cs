using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        if (timerText != null)
            timerText.text = FormatTime(elapsedTime);
    }

    public void StopTimer()
    {
        isRunning = false;

        if (GameData.Instance != null)
        {
            GameData.Instance.finalTime = elapsedTime;
            Debug.Log($"[Timer] Saved time to GameData: {elapsedTime:0.00}s");
        }
        else
        {
            Debug.LogWarning("[Timer] GameData not found!");
        }
    }

    public float GetSeconds() => elapsedTime;

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time % 60f;
        return $"{minutes:00}:{seconds:00.00}";
    }
}
