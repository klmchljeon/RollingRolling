using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class M2_GameOverScene : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;
    [SerializeField]
    private TMP_Text best_score;
    [SerializeField]
    private Button retry;
    [SerializeField]
    private Button exit;


    // Start is called before the first frame update
    void Start()
    {
        M2_Judgement.bestScore = PlayerPrefs.GetInt("M2_Score", 0);
        score.text = $"Score: {M2_Judgement.score}";
        best_score.text = $"Best: {M2_Judgement.bestScore}";
    }

    private void OnEnable()
    {
        retry.onClick.AddListener(Retry);
        exit.onClick.AddListener(MainScene);
    }

    private void OnDisable()
    {
        retry.onClick.RemoveListener(Retry);
        exit.onClick.RemoveListener(MainScene);
    }

    void Retry()
    {
        SceneManager.LoadScene("minigame2");
    }

    void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
