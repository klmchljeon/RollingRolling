using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_MoveAim : MonoBehaviour
{
    public Transform center;      // 궤도의 중심 오브젝트
    public float radius = 5f;     // 궤도의 반지름
    public float speed = 2f;      // 궤도 운동 속도 (도/초)
    public float aimangle = 0f;   // 현재 각도 (도)

    public bool isClockwise = true;  // true면 시계 방향, false면 반시계 방향

    void Update()
    {
        // 방향에 따라 각도 변경
        float direction = isClockwise ? 1f : -1f;
        aimangle += direction * speed * Time.deltaTime;

        // 각도는 0~360도로 제한
        aimangle = (aimangle + 360f) % 360f;

        // 각도 기반 위치 계산
        float aimrad = aimangle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(aimrad), Mathf.Sin(aimrad), 0f) * radius;
        transform.position = center.position + offset;
    }

    // 외부에서 호출: 방향 반전
    public void ToggleDirection()
    {
        isClockwise = !isClockwise;
    }

    // 현재 각도 반환
    public float GetCurrentAngle()
    {
        return aimangle;
    }
}
