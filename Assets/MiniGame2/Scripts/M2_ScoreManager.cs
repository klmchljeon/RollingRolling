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
        // 13�� �ʰ��� ��쿡�� 0��

        totalScore += score;
        Debug.Log($"�� ����: {totalScore}");

        return score;

    }

    public void ResetScore()
    {
        totalScore = 0;
    }
}
