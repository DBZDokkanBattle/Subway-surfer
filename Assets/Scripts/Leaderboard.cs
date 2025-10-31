using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [Header("UI Slots (top N rows)")]
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;   // shows time as mm:ss.ff

    [Header("Config")]
    [SerializeField] private string publicLeaderboardKey = "6f2f429158f948114e7dbcd5d279dabddf1d51a2cd225750e88ae125cc8f6bbb";

    // Must match whatever you used to convert time -> score in ScoreSubmitter (100000 / seconds)
    private const float SCORE_SCALE = 100000f;

    private void Start()
    {
        GetLeaderboard();
    }

    /// <summary>
    /// Pulls entries from the online board and fills the UI.
    /// Stored "score" is inverse time; we convert back to seconds for display.
    /// </summary>
    public void GetLeaderboard()
    {
        // Optional: show loading placeholders
        SetAllRows("-", "...");

        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, (entries) =>
        {
            if (entries == null || entries.Length == 0)
            {
                SetAllRows("-", "-");
                return;
            }

            int loopLength = Mathf.Min(entries.Length, names.Count);

            for (int i = 0; i < loopLength; ++i)
            {
                var e = entries[i];
                names[i].text = SafeName(e.Username);

                // Convert stored score -> seconds
                float seconds = ScoreToSeconds(e.Score);
                scores[i].text = FormatAsClock(seconds);
            }

            // Clear remaining empty rows (if any)
            for (int i = loopLength; i < names.Count; i++)
            {
                names[i].text = "-";
                scores[i].text = "-";
            }
        });
    }

    /// <summary>
    /// Upload an entry (score should be the inverted time you computed elsewhere),
    /// then refresh the board.
    /// </summary>
    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, SafeName(username), score, (msg) =>
        {
            GetLeaderboard();
        });
    }

    // ---------- Helpers ----------

    private static string SafeName(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "Player";
        return raw.Trim();
    }

    private float ScoreToSeconds(int score)
    {
        // Avoid division by zero in case a 0 slips in
        int safeScore = Mathf.Max(1, score);
        return SCORE_SCALE / safeScore;
    }

    // mm:ss.ff (hundredths)
    private string FormatAsClock(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        float sec = seconds % 60f;
        return $"{minutes:00}:{sec:00.00}";
    }

    private void SetAllRows(string nameText, string scoreText)
    {
        int count = Mathf.Min(names.Count, scores.Count);
        for (int i = 0; i < count; i++)
        {
            names[i].text = nameText;
            scores[i].text = scoreText;
        }
    }
}
