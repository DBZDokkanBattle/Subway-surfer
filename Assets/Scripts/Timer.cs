using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    [Header("UI Component")]
    public TextMeshProUGUI timerText;   // Drag a TMP text here if you want to see the time on screen

    [Header("Timer Settings")]
    public float currentTime = 0f;      // Starts at 0 seconds
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

        if (hasFormat)
            timerText.text = currentTime.ToString(timeFormats[format]);
        else
            timerText.text = currentTime.ToString();
    }

    // ---------- Public helper methods ----------

    // Reset timer to 0 and start again
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
