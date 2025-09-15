using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class M1_stonerotation : MonoBehaviour

{
    // Start is called before the first frame update
    public Transform centerObject;     // 중심이 될 기준 오브젝트
    public float orbitRadius = 2f;     // 원의 반지름
    public float orbitSpeed = 50f;     // 회전 속도 (도/초)

    private float angle = 0f;

    public float startAngle = 0f;

    public int direction = 1;

    private Vector3 originalPos;
    private Quaternion originalRotation;

    void Awake()
    {
        originalPos = transform.position;
        originalRotation = transform.rotation;
    }

    public void SetDirection(int dir)
    {
        direction = dir;
        if (centerObject != null)
        {
            if (dir == -1)
            {
                Vector3 flipped = new Vector3(-originalPos.x, originalPos.y, originalPos.z);
                transform.position = flipped;
                transform.rotation = Quaternion.Euler(0, 0, -originalRotation.z);
                angle = 3.14f;
            }
            else
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }
    }

    
    void Start()
    {
         /*if (centerObject != null)
        {
            angle = startAngle * Mathf.Deg2Rad; // 도-> 라디안 변환
            float x = centerObject.position.x + Mathf.Cos(angle) * orbitRadius;
            float y = centerObject.position.y + Mathf.Sin(angle) * orbitRadius;

            transform.position = new Vector3(x, y, transform.position.z);
            // 시작 위치에서 중심까지의 방향 계산 → 각도 보정
            Vector3 offset = transform.position - centerObject.position;
            angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (centerObject == null)
            return;

        angle += orbitSpeed * direction * Time.deltaTime; //direction 추가로 곱함

        float x = centerObject.position.x + Mathf.Cos(angle) * orbitRadius;
        float y = centerObject.position.y + Mathf.Sin(angle) * orbitRadius;
        transform.position = new Vector3(x,y,transform.position.z);


        // 중심 방향을 바라보게 회전 설정
        Vector3 directionToCenter = centerObject.position - transform.position;

        // Z축 기준 2D 회전 (오브젝트가 "오른쪽(x+)"을 앞이라고 가정할 때)
        float angleToCenter = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleToCenter+90f);
        
    }

}
