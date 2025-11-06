using UnityEngine;
using TMPro;

public class ScoreSubmitter : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField inputName;     
    [SerializeField] private TextMeshProUGUI inputScore;   
    [SerializeField] private Leaderboard leaderboard;       

    private const float SCORE_SCALE = 100000f;
    private float finalTime;

    private void Start()
    {
        // get final time from GameData
        finalTime = (GameData.Instance != null) ? GameData.Instance.finalTime : 0f;
        Debug.Log($"[ScoreSubmitter] Loaded time = {finalTime:0.00}s");

        // show it on screen
        if (inputScore != null)
        {
            int m = Mathf.FloorToInt(finalTime / 60f);
            float s = finalTime % 60f;
            inputScore.text = $"{m:00}:{s:00.00}";
        }
    }

    // called by the Submit button
    public void SubmitScore()
    {
        if (leaderboard == null)
        {
            Debug.LogWarning("[ScoreSubmitter] Leaderboard reference missing.");
            return;
        }

        if (finalTime <= 0f)
        {
            Debug.LogWarning("[ScoreSubmitter] No valid time to submit!");
            return;
        }

        string name = string.IsNullOrWhiteSpace(inputName?.text) ? "Player" : inputName.text.Trim();
        PlayerPrefs.SetString("PlayerName", name); // remember for next time

        // convert time -> score
        int score = Mathf.RoundToInt(SCORE_SCALE / Mathf.Max(0.01f, finalTime));

        Debug.Log($"[ScoreSubmitter] Submitting {name} → {finalTime:0.00}s → score {score}");
        leaderboard.SetLeaderboardEntry(name, score);
    }
}