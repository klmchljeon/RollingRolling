using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1_Score : MonoBehaviour
{
    public Text scoreText;
    public float scoreincreasePerSecond =0.4f;
    public static int score;
    public static int bestscore;
    private float scoreAccumulator;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreAccumulator = 0f;
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        scoreAccumulator += Time.deltaTime * scoreincreasePerSecond;
        if (scoreAccumulator >= 1f)
        {
            int increase = (int)scoreAccumulator;
            score += increase;
            scoreAccumulator -= increase;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = ""+ score;
    }
}
