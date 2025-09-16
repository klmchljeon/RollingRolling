using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_Player_Stand : MonoBehaviour
{
    public Transform circle; // <- ���⼭ circle: ȸ�� ���� ���� ������Ʈ�� Transform
    private Vector3 offset;
    private float previousAngle;
    // Start is called before the first frame update
    void Start()
    {
        offset = (transform.position - circle.position) + new Vector3(0.1f,0.1f,0f);
        Debug.Log(offset);
        previousAngle = circle.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        float currentAngle = circle.rotation.eulerAngles.z;
        float deltaAngle = currentAngle - previousAngle;
        //ȸ�� (��ȭ) ����
        //������ ȸ������ �������� offset�� ȸ��
        offset = Quaternion.Euler(0, 0, deltaAngle) * offset * 1.0005f;
        transform.position = circle.position + offset;
        // ���� �������� ���� ���� ���� ����
        previousAngle = currentAngle;
    }
}