using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;  // from the plugin

public class Leaderboard : MonoBehaviour
{
    [Header("UI Rows (same order, same count)")]
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    [Header("Public Key from the website")]
    [SerializeField] private string publicLeaderboardKey = "PASTE_YOUR_KEY_HERE";

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, entries =>
        {
            int rowCount = Mathf.Min(entries.Length, names.Count, scores.Count);

            for (int i = 0; i < rowCount; i++)
            {
                var e = entries[i];
                names[i].text = string.IsNullOrEmpty(e.Username) ? "-" : e.Username;

                // We will store times as NEGATIVE milliseconds so "faster" goes on top.
                int milliseconds = -e.Score; // flip back to positive
                scores[i].text = FormatMilliseconds(milliseconds);
            }

            // Clear extra rows if our UI has more than the server returned
            for (int i = rowCount; i < Mathf.Min(names.Count, scores.Count); i++)
            {
                names[i].text = "-";
                scores[i].text = "-";
            }
        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, _ =>
        {
            GetLeaderboard(); // refresh after upload
        });
    }

    // Same display as your timer: MM:SS:CC
    public static string FormatMilliseconds(int ms)
    {
        if (ms < 0) ms = -ms;
        int totalSeconds = ms / 1000;
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        int centis = (ms % 1000) / 10;
        return $"{minutes:00}:{seconds:00}:{centis:00}";
    }
}
