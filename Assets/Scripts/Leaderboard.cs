using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [Header("Dan Leaderboard")]
    [SerializeField]
    private string publicLeaderboardKey =
        "6f2f429158f948114e7dbcd5d279dabddf1d51a2cd225750e88ae125cc8f6bbb";

    [Header("UI (Top N rows)")]
    [Tooltip("Parallel lists: element i in names matches element i in scores")]
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores; // displays mm:ss.ff

    private const float SCORE_SCALE = 100000f;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        SetAllRows("-", "...");
        Debug.Log("[LB] Refreshing leaderboard… key=" + publicLeaderboardKey);

        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, entries =>
        {
            if (entries == null || entries.Length == 0)
            {
                Debug.LogWarning("[LB] No entries returned.");
                SetAllRows("-", "-");
                return;
            }

            int rowSlots = Mathf.Min(names.Count, scores.Count);
            int count = Mathf.Min(entries.Length, rowSlots);
            Debug.Log($"[LB] Received {entries.Length} entries (showing {count}).");

            for (int i = 0; i < count; i++)
            {
                var e = entries[i];
                string user = SafeName(e.Username);
                float seconds = ScoreToSeconds(e.Score);

                names[i].text = user;
                scores[i].text = FormatAsClock(seconds);

                Debug.Log($"[LB] {i + 1}. {user} | rawScore={e.Score} -> {seconds:0.00}s");
            }

            // clear unused rows
            for (int i = count; i < rowSlots; i++)
            {
                names[i].text = "-";
                scores[i].text = "-";
            }
        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        Submit(username, score);
    }

    public void Submit(string username, int score)
    {
        string clean = SafeName(username);
        Debug.Log($"[LB] SUBMIT as NEW member: '{clean}' score={score}");

        // diagnostics before clearing
        Debug.Log("[LB] Before wipe: has MemberID=" + PlayerPrefs.HasKey("Dan_Leaderboard_MemberID"));
        Debug.Log("[LB] Before wipe: has PublicKey=" + PlayerPrefs.HasKey("Dan_Leaderboard_MemberPublicKey"));
        Debug.Log("[LB] Using leaderboard key: " + publicLeaderboardKey);

        // clear local identity
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("[LB] PlayerPrefs.DeleteAll() -> local identity wiped.");

        // diagnostics after clearing
        Debug.Log("[LB] After wipe: has MemberID=" + PlayerPrefs.HasKey("Dan_Leaderboard_MemberID"));
        Debug.Log("[LB] After wipe: has PublicKey=" + PlayerPrefs.HasKey("Dan_Leaderboard_MemberPublicKey"));

        // upload new entry
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, clean, score, msg =>
        {
            Debug.Log("[LB] Upload result: " + msg);
            StartCoroutine(RefreshAfterDelay(0.5f)); // small delay before refresh
        });
    }

    private System.Collections.IEnumerator RefreshAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Refresh();
    }

    private static string SafeName(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "Player";
        raw = raw.Trim();
        if (raw.Length > 20) raw = raw.Substring(0, 20);
        return raw;
    }

    private float ScoreToSeconds(int score)
    {
        int safe = Mathf.Max(1, score);
        return SCORE_SCALE / safe;
    }

    private string FormatAsClock(float seconds)
    {
        int m = Mathf.FloorToInt(seconds / 60f);
        float s = seconds % 60f;
        return $"{m:00}:{s:00.00}";
    }

    private void SetAllRows(string nameText, string scoreText)
    {
        int n = Mathf.Min(names.Count, scores.Count);
        for (int i = 0; i < n; i++)
        {
            names[i].text = nameText;
            scores[i].text = scoreText;
        }
    }
}
