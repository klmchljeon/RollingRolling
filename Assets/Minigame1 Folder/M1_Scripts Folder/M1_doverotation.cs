using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class M1_doverotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform centerObject1;     // 중심이 될 기준 오브젝트
    public float orbitRadius1 = 2f;     // 원의 반지름
    public float orbitSpeed1 = 50f;     // 회전 속도 (도/초)

    private float angle1 = 0f;

    public float startAngle1 = 0f;
    public int direction1 = 1; // 얘 관련된놈들이 추가한거임(SetDirection)
    private Vector3 originalPos1;
    private Quaternion originalRotation1;

    void Awake()
    {
        originalPos1 = transform.position;
        originalRotation1 = transform.rotation;
    }

    public void SetDirection1(int dir)
    {
        direction1 = dir;
        if (centerObject1 != null)
        {
            if (dir == -1)
            {
                Vector3 flipped1 = new Vector3(-originalPos1.x, originalPos1.y, originalPos1.z);
                transform.position = flipped1;
                transform.rotation = Quaternion.Euler(0, 0, -originalRotation1.z);
            }
            else
            {
                transform.position = originalPos1;
                transform.rotation = originalRotation1;
            }
        }
    }

    
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (centerObject1 == null)
            return;

        angle1 += orbitSpeed1 * direction1 * Time.deltaTime; //direction1 추가로 곱함
        //if (angle >= 360f) angle -= 360f;

        // 라디안으로 변환
        //float radians = angle * Mathf.Deg2Rad;

        // 새로운 위치 계산 (X-Z 평면 기준)
        //float x = Mathf.Cos(radians) * orbitRadius;
        //float y = Mathf.Sin(radians) * orbitRadius;
        float x = centerObject1.position.x + Mathf.Cos(angle1) * orbitRadius1;
        float y = centerObject1.position.y + Mathf.Sin(angle1) * orbitRadius1;
        transform.position = new Vector3(x,y,transform.position.z);

        // 위치 업데이트
        //transform.position = new Vector3(
            //centerObject.position.x + x,
            //transform.position.z,
            //centerObject.position.y + y

        //);

        // 중심 방향을 바라보게 회전 설정
        Vector3 directionToCenter = centerObject1.position - transform.position;

        // Z축 기준 2D 회전 (오브젝트가 "오른쪽(x+)"을 앞이라고 가정할 때)
        float angleToCenter = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleToCenter+90f);
        
    }

}
