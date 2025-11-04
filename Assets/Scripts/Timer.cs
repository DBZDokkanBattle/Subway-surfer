using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    [Header("UI Component")]
    public TextMeshProUGUI timerText;   

    [Header("Timer Settings")]
    public float currentTime = 0f;      
    public bool countDown = false;      // false = count up (stopwatch), true = countdown
    public bool running = true;         // controls if timer is ticking

    [Header("Format Settings")]
    public bool hasFormat = true;
    public TimerFormats format = TimerFormats.HundredthDecimal;

    [Header("Events")]
    public UnityEvent onTimerEnded;     // you can hook this up to call things when time runs out (optional)

    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();
    private bool endedInvoked;

    private void Awake()
    {
        // Define number formats
        timeFormats = new Dictionary<TimerFormats, string>
        {
            { TimerFormats.Whole, "0" },
            { TimerFormats.TenthDecimal, "0.0" },
            { TimerFormats.HundredthDecimal, "0.00" }
        };
    }

    private void Update()
    {
        if (!running) return;

        // Increase or decrease time
        currentTime += countDown ? -Time.deltaTime : Time.deltaTime;

        // Stop if counting down and reached 0
        if (countDown && currentTime <= 0f)
        {
            currentTime = 0f;
            running = false;

            if (!endedInvoked)
            {
                endedInvoked = true;
                onTimerEnded?.Invoke(); // calls anything hooked in the Inspector
            }
        }

        // Update on-screen text
        SetTimerText();
    }

    private void SetTimerText()
    {
        if (timerText == null) return;

        // Split currentTime into minutes, seconds, hundredths
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        int hundredths = Mathf.FloorToInt((currentTime * 100f) % 100f); // 0–99

        // Format: mm:ss.ff
        timerText.text = $"{minutes:00}:{seconds:00}.{hundredths:00}";
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        running = true;
        endedInvoked = false;
    }

    // Pause the timer
    public void PauseTimer() => running = false;

    // Resume the timer
    public void ResumeTimer() => running = true;

    // Get time in seconds (for ScoreSubmitter)
    public float GetSeconds() => Mathf.Max(0f, currentTime);
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundredthDecimal
}
