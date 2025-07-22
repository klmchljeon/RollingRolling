using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1_bestscore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "BestScore : " + M1_Score.bestscore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
