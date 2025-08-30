using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_AimController : MonoBehaviour
{
    public Transform center; // ȸ�� �߽� (�÷��̾�)
    public Transform circle;
    public float radius = 2f;
    public float rotationSpeed = 90f; // degrees per second
    private Vector3 offset;

    private float angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        M3_Move tmp = circle.GetComponent<M3_Move>();
        offset = center.position - circle.position;

        Vector3 p = new Vector3(-offset.y, offset.x, offset.z);

        float magnitude = offset.magnitude; // ũ��

        transform.position = center.position + p*tmp.speed*magnitude; //������ ��ġ

        

        //angle += rotationSpeed * Time.deltaTime;
        //float rad = angle * Mathf.Deg2Rad;

        //Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
        //transform.position = center.position + offset;
    }
}
