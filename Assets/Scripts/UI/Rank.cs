using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rank : MonoBehaviour
{
    Leaderboard leaderboard;
    public List<LapCounter> lapCounters = new List<LapCounter>();

    private void Awake()
    {
        LapCounter[] lapCounterArr = FindObjectsOfType<LapCounter>();

        lapCounters = lapCounterArr.ToList<LapCounter>();

        foreach (LapCounter counters in lapCounters)
            counters.OnPass += OnPass;

        leaderboard = FindObjectOfType<Leaderboard>();
    }
    private void Start()
    {
        leaderboard.UpdateList(lapCounters);
    }

    void OnPass(LapCounter lapCounter)
    {
        lapCounters = lapCounters.OrderByDescending(s => s.GetPassedCheckpoints()).ThenBy(s => s.GetCheckpointTime()).ToList();

        int carRank = lapCounters.IndexOf(lapCounter) + 1;

        lapCounter.SetCarRank(carRank);

        leaderboard.UpdateList(lapCounters);
    }
}   
