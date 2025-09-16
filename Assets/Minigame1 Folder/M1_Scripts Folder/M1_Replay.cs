using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class M1_Replay : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("MIniGame1");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
