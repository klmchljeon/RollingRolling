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

    
    void Start()
    {
         if (centerObject1 != null)
        {
            angle1 = startAngle1 * Mathf.Deg2Rad; // 도-> 라디안 변환
            float x = centerObject1.position.x + Mathf.Cos(angle1) * orbitRadius1;
            float y = centerObject1.position.y + Mathf.Sin(angle1) * orbitRadius1;

            transform.position = new Vector3(x, y, transform.position.z);
            // 시작 위치에서 중심까지의 방향 계산 → 각도 보정
            Vector3 offset = transform.position - centerObject1.position;
            angle1 = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (centerObject1 == null)
            return;

        angle1 += orbitSpeed1 * Time.deltaTime;
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
