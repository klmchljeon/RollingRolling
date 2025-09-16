using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m1score;
    [SerializeField] private TMP_Text m2score;
    [SerializeField] private TMP_Text m3score;

    [SerializeField] private Button game1;
    [SerializeField] private Button game2;
    [SerializeField] private Button game3;

    // Start is called before the first frame update
    void Start()
    {

        // 불러오기(없으면 0)
        M1_Score.bestscore = PlayerPrefs.GetInt("M1_Score", 0);
        M2_Judgement.bestScore = PlayerPrefs.GetInt("M2_Score", 0);
        //scoreC = PlayerPrefs.GetInt("MiniGame_C_Score", 0);

        m1score.text = $"Best: {M1_Score.bestscore}";
        m2score.text = $"Best: {M2_Judgement.bestScore}";
    }

    private void OnEnable()
    {
        game1.onClick.AddListener(Game1);
        game2.onClick.AddListener(Game2);
    }

    private void OnDisable()
    {
        game1.onClick.RemoveListener(Game1);
        game2.onClick.RemoveListener(Game2);
    }


    void Game1()
    {
        SceneManager.LoadScene("MiniGame1");
    }

    void Game2()
    {
        SceneManager.LoadScene("minigame2");
    }
}
