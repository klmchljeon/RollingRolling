using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_ScoreManager : MonoBehaviour
{
    public int totalScore { get; private set; } = 0;

    public int CalculateScore(float realDiff)
    {
        int score = 0;
        if (realDiff <= 4f)
        {
            score = 30;
        }
        else if (realDiff <= 9f)
        {
            score = 20;
        }
        else if (realDiff <= 13f)
        {
            score = 10;
        }
        // 13도 초과한 경우에는 0점

        totalScore += score;
        Debug.Log($"총 점수: {totalScore}");

        return score;

    }

    public void ResetScore()
    {
        totalScore = 0;
    }
}
