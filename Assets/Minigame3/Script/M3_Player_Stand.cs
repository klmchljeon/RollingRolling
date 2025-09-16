using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_Player_Stand : MonoBehaviour
{
    public Transform circle; // <- 여기서 circle: 회전 중인 원판 오브젝트의 Transform
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
        //회전 (변화) 적용
        //원판의 회전값을 기준으로 offset을 회전
        offset = Quaternion.Euler(0, 0, deltaAngle) * offset * 1.0005f;
        transform.position = circle.position + offset;
        // 다음 프레임을 위해 현재 각도 저장
        previousAngle = currentAngle;
    }
}