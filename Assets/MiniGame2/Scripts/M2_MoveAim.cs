using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class M2_MoveAim : MonoBehaviour

{
    public Transform center;  // 궤도의 중심 오브젝트
    public float radius = 5f; // 궤도의 반지름
    public float speed = 2f;  // 궤도 운동 속도
    private float angle = 0f;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime;
        Vector3 newPosition = center.position + Vector3.RotateTowards(transform.position, center.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f), 1, Time.deltaTime * 100f);
        transform.position = newPosition;
    }
}
