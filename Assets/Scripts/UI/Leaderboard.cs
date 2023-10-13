using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardItem;

    SetLeaderboard[] setLeaderboard;

    private void Awake()
    {
        VerticalLayoutGroup layoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        LapCounter[] lapCounterArr = FindObjectsOfType<LapCounter>();

        setLeaderboard = new SetLeaderboard[lapCounterArr.Length];

        for(int i = 0; i < lapCounterArr.Length; i++)
        {
            GameObject leaderboardObj = Instantiate(leaderboardItem, layoutGroup.transform);
            setLeaderboard[i] = leaderboardObj.GetComponent<SetLeaderboard>();
            setLeaderboard[i].SetRankText($"{i + 1}.");
             
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateList(List<LapCounter> lapCounters)
    {
        for (int i = 0; i < lapCounters.Count; i++)
        {
            setLeaderboard[i].SetIDText(lapCounters[i].gameObject.name);
        }
    }
}
