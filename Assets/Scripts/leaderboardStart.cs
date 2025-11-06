using UnityEngine;

public class LeaderboardStart : MonoBehaviour
{
    void Start()
    {
        var timer = FindFirstObjectByType<TimerToLeaderboard>();
        if (timer != null) timer.LoadAndDisplayFinalTime();
    }
}
