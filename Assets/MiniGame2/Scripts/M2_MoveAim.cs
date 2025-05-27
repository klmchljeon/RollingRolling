using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_MoveAim : MonoBehaviour
{
    public Transform center;      // �˵��� �߽� ������Ʈ
    public float radius = 5f;     // �˵��� ������
    public float speed = 2f;      // �˵� � �ӵ� (��/��)
    public float aimangle = 0f;   // ���� ���� (��)

    public bool isClockwise = true;  // true�� �ð� ����, false�� �ݽð� ����

    void Update()
    {
        // ���⿡ ���� ���� ����
        float direction = isClockwise ? 1f : -1f;
        aimangle += direction * speed * Time.deltaTime;

        // ������ 0~360���� ����
        aimangle = (aimangle + 360f) % 360f;

        // ���� ��� ��ġ ���
        float aimrad = aimangle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(aimrad), Mathf.Sin(aimrad), 0f) * radius;
        transform.position = center.position + offset;
    }

    // �ܺο��� ȣ��: ���� ����
    public void ToggleDirection()
    {
        isClockwise = !isClockwise;
    }

    // ���� ���� ��ȯ
    public float GetCurrentAngle()
    {
        return aimangle;
    }
}
