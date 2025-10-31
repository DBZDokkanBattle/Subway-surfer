using UnityEngine;
using TMPro;

public class ScoreSubmitter : MonoBehaviour
{
    [SerializeField] private Timer timer;                 // your Timer script
    [SerializeField] private TMP_InputField inputName;    // the name box
    [SerializeField] private Leaderboard leaderboard;     // the script above

    // Hook this to your Submit button
    public void Submit()
    {
        if (timer == null || leaderboard == null) return;

        string username = (inputName != null && !string.IsNullOrWhiteSpace(inputName.text))
            ? inputName.text.Trim()
            : "Player";

        // Upload NEGATIVE milliseconds so fastest time ranks highest
        int ms = Mathf.Max(0, timer.GetMilliseconds());
        int score = -ms;

        leaderboard.SetLeaderboardEntry(username, score);
        Debug.Log($"Submitted {username}: {FormatMilliseconds(ms)} (stored {score})");
    }

    private static string FormatMilliseconds(int ms)
    {
        if (ms < 0) ms = -ms;
        int total = ms / 1000;
        int minutes = total / 60;
        int seconds = total % 60;
        int centis = (ms % 1000) / 10;
        return $"{minutes:00}:{seconds:00}:{centis:00}";
    }
}
