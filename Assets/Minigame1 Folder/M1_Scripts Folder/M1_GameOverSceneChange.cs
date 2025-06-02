using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M1_GameOverSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    bool isGameOver = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameOver) return;
        if (collision.gameObject.CompareTag("Ground"))
        {
            return;
        }

        if (M1_Score.score > M1_Score.bestscore)
        {
            M1_Score.bestscore = M1_Score.score;
        }

        SceneManager.LoadScene("M1_GameOverScene");
    }
}
