using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [Tooltip("Starting value in seconds.")]
    [SerializeField] private float currentTime = 0f;
    [Tooltip("If true, time decreases toward zero; otherwise it counts up.")]
    [SerializeField] private bool countDown = false;
    [Tooltip("Start running automatically on Start().")]
    [SerializeField] private bool autoStart = true;
    [Tooltip("Use unscaled time (ignores Time.timeScale).")]
    [SerializeField] private bool useUnscaledTime = false;

    [Header("Display")]
    [SerializeField] private DisplayMode displayMode = DisplayMode.MM_SS_CC; // pretty default
    [SerializeField] private int decimals = 2; // used only for RawSeconds mode

    [Header("Events")]
    public UnityEvent onTimerEnded;   // fired once when countdown reaches zero
    public UnityEvent<float> onTimerUpdated; // passes currentTime each tick (optional)

    private bool isRunning = false;
    private bool endedInvoked = false;

    private void Awake()
    {
        // Ensure initial text is shown in editor & at runtime before Start()
        UpdateText();
    }

    private void Start()
    {
        if (autoStart)
        {
            isRunning = true;
            endedInvoked = false;
        }
    }

    private void Update()
    {
        if (!isRunning) return;

        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        currentTime += countDown ? -dt : dt;

        if (countDown && currentTime <= 0f)
        {
            currentTime = 0f;
            UpdateText();
            onTimerUpdated?.Invoke(currentTime);

            if (!endedInvoked)
            {
                endedInvoked = true;
                onTimerEnded?.Invoke();
            }
            isRunning = false; // stop ticking
            return;
        }

        UpdateText();
        onTimerUpdated?.Invoke(currentTime);
    }

    private void UpdateText()
    {
        if (timerText == null) return;

        switch (displayMode)
        {
            case DisplayMode.RawSeconds:
                timerText.text = currentTime.ToString("0." + new string('0', Mathf.Clamp(decimals, 0, 3)),
                                                     CultureInfo.InvariantCulture);
                break;

            case DisplayMode.MM_SS:
                timerText.text = FormatClock(currentTime, includeCentis: false);
                break;

            case DisplayMode.MM_SS_CC:
                timerText.text = FormatClock(currentTime, includeCentis: true);
                break;
        }
    }

    private static string FormatClock(float seconds, bool includeCentis)
    {
        if (seconds < 0f) seconds = -seconds;
        int totalMs = Mathf.FloorToInt(seconds * 1000f);
        int mins = (totalMs / 1000) / 60;
        int secs = (totalMs / 1000) % 60;

        if (!includeCentis)
            return $"{mins:00}:{secs:00}";

        int centis = (totalMs % 1000) / 10;
        return $"{mins:00}:{secs:00}:{centis:00}";
    }

    // -------- Public API for UI/buttons --------
    public void StartTimer(float startValueSeconds = 0f, bool startCountingDown = false)
    {
        currentTime = startValueSeconds;
        countDown = startCountingDown;
        endedInvoked = false;
        isRunning = true;
        UpdateText();
    }

    public void StopTimer() { isRunning = false; }
    public void PauseTimer() { isRunning = false; }
    public void ResumeTimer() { if (!endedInvoked) isRunning = true; }
    public void ResetTimer(float valueSeconds = 0f)
    {
        currentTime = valueSeconds;
        endedInvoked = false;
        UpdateText();
    }

    public float GetSeconds() => Mathf.Max(0f, currentTime);
    public int GetMilliseconds() => Mathf.Max(0, Mathf.FloorToInt(currentTime * 1000f));

    // Optional helpers to change settings at runtime
    public void SetDisplayMode(int mode) => displayMode = (DisplayMode)mode;
    public void UseUnscaledTime(bool v) => useUnscaledTime = v;

    public enum DisplayMode
    {
        RawSeconds,   // e.g., 12.34
        MM_SS,        // 01:23
        MM_SS_CC      // 01:23:45 (centiseconds)
    }
}
