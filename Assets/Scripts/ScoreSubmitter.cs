using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreSubmitter : MonoBehaviour
{
    [Header("UI (optional display)")]
    [SerializeField] private TextMeshProUGUI inputScore;   
    [SerializeField] private TMP_InputField inputName;

    [Header("Refs")]
    [SerializeField] private Timer timer;                  
    public UnityEvent<string, int> submitScoreEvent;

    private const float SCORE_SCALE = 100000f;

    private void Awake()
    {
        if (timer == null) timer = GetComponentInParent<Timer>(true); 
    }

    public void SubmitScore()
    {
        if (timer == null)
        {
            Debug.LogWarning("ScoreSubmitter: Timer reference not set (no Timer on this Canvas?).");
            return;
        }

        float seconds = Mathf.Max(0.01f, timer.GetSeconds());    
        int convertedScore = Mathf.RoundToInt(SCORE_SCALE / seconds);

        if (inputScore != null) inputScore.text = FormatAsClock(seconds);

        string nameToUse = string.IsNullOrWhiteSpace(inputName?.text) ? "Player" : inputName.text.Trim();
        submitScoreEvent?.Invoke(nameToUse, convertedScore);
    }

    
    private string FormatAsClock(float s)
    {
        int m = Mathf.FloorToInt(s / 60f);
        float sec = s % 60f;
        return $"{m:00}:{sec:00.00}";
    }
}
