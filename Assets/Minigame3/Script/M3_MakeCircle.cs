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

    public GameObject player; //플레이어 연결용 추가(1차)
    public M3_Player_Stand playerScript; //플레이어 회전 스크립트(불러오기 추가)(1차)
    public AimController aimController; //조준점 스크립트 연결용 추가(1차)

    //플레이어가 현재 서있는 원판 추가(2차 수정 중 새로 추가)
    private GameObject playerCurrentCircle = null;

    private List<GameObject> circles = new List<GameObject>(); //원판 리스트 추가(1차 수정)
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

        //회전 속도 0이 되지 않도록 조정(3차 수정)
        float speed = 0f;
        while (Mathf.Abs(speed) < 0.5f)
            speed = Random.Range(-2f, 2f);
        // 이전 코드: new Vector3(4, Random.Range(-2.86f, 2.46f), 0);
        newcircle.GetComponent<M3_Move>().speed = speed;

        newcircle.transform.localScale = new Vector3(diameter, diameter, 1f);
        newcircle.SetActive(true);//이건 GPT 탐색
        timer = 0;

        //플레이어 조준선 활성화(4차 수정)
        player.SetActive(true);
        aimController.gameObject.SetActive(true);

        //플레이어 초기 위치 및 설정 추가(1차)
        player.transform.position = spawnPosition + Vector3.up * 0.05f; //살짝 위에 착지 -> 변경(0.5 -> 0.05)(7.29)
        playerScript.circle = newcircle.transform;
        playerScript.enabled = true;

        //조준점에도 초기 원판 정보 연결 추가(1차)
        aimController.center = player.transform;
        aimController.circle = newcircle.transform;

        //현재 원판 저장(2차 수정 중 추가)
        playerCurrentCircle = newcircle;

        //리스트에 등록(1차 추가)
        circles.Add(newcircle);

        // 이전 원 정보 업데이트(2차 수정 중 추가)
        prevPosition = Vector3.zero;
        prevDiameter = diameter;

        Destroy(newcircle, 10.0f); //첫 원판도 삭제됨(조건 추가 후 조정)
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

        //회전 속도 0이 되지 않도록 조정(3차 수정)
        float speed = 0f;
        while (Mathf.Abs(speed) < 0.5f)
            speed = Random.Range(-2f, 2f);
        newcircle.GetComponent<M3_Move>().speed = speed;

        newcircle.transform.localScale = new Vector3(diameter, diameter, 1f);
        newcircle.SetActive(true);//이건 GPT 탐색
        timer = 0;

        //리스트 추가(새로 추가됨)(1차)
        circles.Add(newcircle);

        // 이전 원 정보 업데이트(2차 수정)
        prevPosition = spawnPosition;
        prevDiameter = diameter;

        if (playerCurrentCircle != newcircle)
            Destroy(newcircle, 10.0f);
    }
    
    //2차 수정 추가
    public void SetCurrentPlayerCircle(GameObject circle)
    {
        playerCurrentCircle = circle;
    }
}