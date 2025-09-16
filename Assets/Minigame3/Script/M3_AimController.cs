using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_AimController : MonoBehaviour
{
    public Transform center;
    public Transform circle;

    [Header("Prediction scale")]
    public float baseBias = 0f;              // |offset|에 더해줄 기본 바이어스
    public float minPredictDistance = 1.5f;   // 최소 거리 보장
    public float maxPredictDistance = 5f;     // 최대 거리 제한

    private void Update()
    {
        if (!center || !circle) return;

        var mover = circle.GetComponent<M3_Move>();
        float speed = mover ? mover.speed : 0f;

        Vector3 offset = center.position - circle.position; // 중심까지의 벡터

        // 수직 방향 (2D 기준 z=0로 고정)
        Vector3 perp = new Vector3(-offset.y, offset.x, 0f);
        Vector3 dir;
        if (perp.sqrMagnitude > 1e-8f)
            dir = perp.normalized;           // 방향만 사용
        else
            dir = Vector3.up;                // offset이 0에 가까울 때의 안전 방향

        // 현재 계산된 "예상 거리" (원래 쓰던 스케일)
        float rawDistance = speed * (offset.magnitude + baseBias);

        // 최소/최대 범위로 클램프
        //Debug.Log(rawDistance);
        float clampedDistance = Mathf.Clamp(rawDistance, minPredictDistance, maxPredictDistance);

        // 최종 예상 지점
        //Debug.Log(clampedDistance);
        transform.position = center.position + dir * clampedDistance;
    }
}
