using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_MakeCircle : MonoBehaviour
{
    public GameObject circle;
    public float minRadius = 0.5f;
    public float maxRadius = 1f;
    public float timeDiff;
    float timer = 0;
    public float Radius2;

    private float prevDiameter = 0f;


    // Start is called before the first frame update
    void Start()
    {
        float randomradius = Random.Range(minRadius, maxRadius);

        float diameter = randomradius * 2f;

        transform.localScale = new Vector3(diameter, diameter, 1f);
    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;
        if (timer > timeDiff)
        {
            float randomradius = Random.Range(minRadius, maxRadius);

            float diameter = randomradius * 2f;
            float angleDeg = Random.Range(-45f, 45f);
            float angleRad = angleDeg * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            //거리에 대한 처리

            //

            GameObject newcircle = Instantiate(circle);
            newcircle.transform.position = new Vector3(4, Random.Range(-2.86f, 2.46f), 0);
            newcircle.GetComponent<M3_Move>().speed = Random.Range(-2f,2f);
            newcircle.transform.localScale = new Vector3(diameter, diameter, 1f);
            timer = 0;


            prevDiameter = diameter;


            Destroy(newcircle, 10.0f);
        }
    }
}
