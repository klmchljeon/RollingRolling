using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_MakeCircle : MonoBehaviour
{
    public GameObject circle;
    public float minRadius = 0.5f;
    public float maxRadius = 1f;
    public float timeDiff;
    public Transform containerTransform;
    float timer = 0;

    private float prevDiameter = 0f;
    private Vector3 prevPosition; //이전 원의 위치 저장

    // Start is called before the first frame update
    void Start()
    {

        float randomradius = Random.Range(minRadius, maxRadius);
        float diameter = randomradius * 2f;

        prevDiameter = diameter;
        prevPosition = transform.position;

        FirstGen();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeDiff)
        {
            Gen();
        }
    }

    void FirstGen()
    {
        float randomradius = Random.Range(minRadius, maxRadius);
        float diameter = randomradius * 2f;

        Vector3 spawnPosition = Vector3.zero;

        GameObject newcircle = Instantiate(circle);
        newcircle.transform.SetParent(containerTransform);
        newcircle.transform.localPosition = spawnPosition;
        // 이전 코드: new Vector3(4, Random.Range(-2.86f, 2.46f), 0);
        newcircle.GetComponent<M3_Move>().speed = Random.Range(-2f, 2f);
        newcircle.transform.localScale = new Vector3(diameter, diameter, 1f);
        newcircle.SetActive(true);//이건 GPT 탐색
        timer = 0;

        // 이전 원 정보 업데이트
        prevDiameter = diameter;
        prevPosition = spawnPosition;


        Destroy(newcircle, 10.0f);
    }
    void Gen()
    {
        float randomradius = Random.Range(minRadius, maxRadius);
        float diameter = randomradius * 2f;

        float angleDeg = Random.Range(-45f, 45f);
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;//normalized 추가됨

        // 거리에 대한 처리
        float distance = (prevDiameter + diameter) * 2.8f; // 코드 수정: 0.5f - > 2.8f, 배수 값 설정에 따른 문제(원 반지름 * 배율)에 맞게 설정
        Vector3 spawnPosition = prevPosition + (Vector3)(direction * distance);

        GameObject newcircle = Instantiate(circle);
        newcircle.transform.SetParent(containerTransform);
        newcircle.transform.localPosition = spawnPosition;
        // 이전 코드: new Vector3(4, Random.Range(-2.86f, 2.46f), 0);
        newcircle.GetComponent<M3_Move>().speed = Random.Range(-2f, 2f);
        newcircle.transform.localScale = new Vector3(diameter, diameter, 1f);
        newcircle.SetActive(true);//이건 GPT 탐색
        timer = 0;

        // 이전 원 정보 업데이트
        prevDiameter = diameter;
        prevPosition = spawnPosition;


        Destroy(newcircle, 10.0f);
    }
}