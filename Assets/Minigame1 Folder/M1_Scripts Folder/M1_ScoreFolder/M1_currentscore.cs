using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class M1_currentscore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "Score :" + M1_Score.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
