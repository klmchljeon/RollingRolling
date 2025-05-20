using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_MakeCircle : MonoBehaviour
{
    public GameObject circle;
    public float radius;
    public float circleRadius;
    public float timeDiff;
    float timer = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeDiff)
        {
            GameObject newcircle = Instantiate(circle);
            newcircle.transform.position = new Vector3(4, Random.Range(-2.86f, 2.46f), 0);
            timer = 0;
            Destroy(newcircle, 10.0f);
        }
    }
}
