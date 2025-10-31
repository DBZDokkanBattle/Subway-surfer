using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TMP_InputField inputName;

    [Header("Events")]
    public UnityEvent<string, int> submitScoreEvent;

    private float elapsedTime = 0f;
    private bool timerRunning = false;

    void Start()
    {
        StartTimer();  // Start automatically (you can remove this if you want manual start)
    }

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        timerRunning = true;
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    /// <summary>
    /// Submits the player's name and elapsed time (in seconds) as score.
    /// </summary>
    public void SubmitScore()
    {
        StopTimer();

        string username = inputName.text;
        int score = Mathf.FloorToInt(elapsedTime); // send time in seconds as score

        submitScoreEvent.Invoke(username, score);
        Debug.Log($"Submitted score: {username} - {elapsedTime:F2} seconds");
    }
}
