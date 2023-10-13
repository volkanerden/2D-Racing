using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{

    public Text rankText;

    int passedCheckpoint = 0;
    int totalPassed = 0;
    float lastCheckpointTime = 0;
    int lapsCompleted = 0;
    const int totalLaps = 2;
    bool raceFinished;
    int carRank = 0;

    public event Action<LapCounter> OnPass;

    public void SetCarRank(int rank)
    {
        carRank = rank;
    }

    public int GetPassedCheckpoints()
    {
        return totalPassed;
    }

    public float GetCheckpointTime()
    {
        return lastCheckpointTime;
    }

    IEnumerator ShowRank(float delay)
    {
        rankText.text = carRank.ToString();

        rankText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);

        rankText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Checkpoint"))
        {
            if (raceFinished)
                return;

            Checkpoint checkPoint = collision.GetComponent<Checkpoint>();
            if (passedCheckpoint + 1 == checkPoint.checkpointNo)
            {
                passedCheckpoint = checkPoint.checkpointNo;
                totalPassed++;

                if(checkPoint.isFinishLine)
                {
                    passedCheckpoint = 0;
                    lapsCompleted++;

                    if (lapsCompleted >= totalLaps)
                        raceFinished = true;
                }

                lastCheckpointTime = Time.deltaTime;
                OnPass?.Invoke(this);

                if (raceFinished)
                    StartCoroutine(ShowRank(100));
                else StartCoroutine(ShowRank(1.5f));
            }
        }
    }
}
