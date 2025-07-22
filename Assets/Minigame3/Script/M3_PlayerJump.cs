using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_PlayerJump : MonoBehaviour
{
    public float jumpforce = 5f; //���� ��ġ
    public LayerMask circleLayer; //���� ����
    public M3_Player_Stand playerStand;
    public AimController aimController;

    private Rigidbody rb;
    private bool isGrounded = true;
    // Start is called before the first frame update
    void Start() => rb = GetComponent<Rigidbody>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            Vector3 dir = (aimController.transform.position - transform.position).normalized;
            rb.velocity = dir * jumpforce;

            playerStand.enabled = false; //ȸ�� �ߴ�
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & circleLayer) != 0)
        {
            // �� ���� ����
            playerStand.circle = col.transform;
            playerStand.enabled = true;

            aimController.circle = col.transform;

            //���� ���� ���� ���
            FindObjectOfType<M3_MakeCircle>().SetCurrentPlayerCircle(col.gameObject);

            isGrounded = true;
        }
    }
}
