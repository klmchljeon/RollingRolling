using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_AimController : MonoBehaviour
{
    public Transform center;
    public Transform circle;

    [Header("Prediction scale")]
    public float baseBias = 0f;              // |offset|�� ������ �⺻ ���̾
    public float minPredictDistance = 1.5f;   // �ּ� �Ÿ� ����
    public float maxPredictDistance = 5f;     // �ִ� �Ÿ� ����

    private void Update()
    {
        if (!center || !circle) return;

        var mover = circle.GetComponent<M3_Move>();
        float speed = mover ? mover.speed : 0f;

        Vector3 offset = center.position - circle.position; // �߽ɱ����� ����

        // ���� ���� (2D ���� z=0�� ����)
        Vector3 perp = new Vector3(-offset.y, offset.x, 0f);
        Vector3 dir;
        if (perp.sqrMagnitude > 1e-8f)
            dir = perp.normalized;           // ���⸸ ���
        else
            dir = Vector3.up;                // offset�� 0�� ����� ���� ���� ����

        // ���� ���� "���� �Ÿ�" (���� ���� ������)
        float rawDistance = speed * (offset.magnitude + baseBias);

        // �ּ�/�ִ� ������ Ŭ����
        //Debug.Log(rawDistance);
        float clampedDistance = Mathf.Clamp(rawDistance, minPredictDistance, maxPredictDistance);

        // ���� ���� ����
        //Debug.Log(clampedDistance);
        transform.position = center.position + dir * clampedDistance;
    }
}
