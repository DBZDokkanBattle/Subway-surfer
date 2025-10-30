using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;

    private string publicKey = "923e62225c92aac66b29f5557b3e18784a408de53c53b05f26a5bc5f3885da82";

    private void Start()
    {
        LoadEntries();
    }

    public void LoadEntries()
    {
        Leaderboards.CircularMayhemLeaderboard.GetEntries(entries =>
        {
            foreach (TextMeshProUGUI name in names)
            {
                name.text = "";
            }

            foreach (var score in scores)
            {
                score.text = "";
            }

            float length = Mathf.min(names.Count, entries.Length);

            for(int i = 0; i < length; i++)
            {
                names[i] = entries[i].Username;
                scores[i] = entries[i].Score.ToString();
            }

        });
    }

    public void SetEntry(string username, int score)
    {
        Leaderboards.RowdyRiversLeaderboard.UploadNewEntry(username, score, isSuccesful =>
        {
            if (isSuccesful)
            {
                LoadEntries();
            }
        });
    }
}
