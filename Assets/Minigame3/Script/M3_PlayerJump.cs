using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_PlayerJump : MonoBehaviour
{
    public float jumpforce = 5f; //점프 수치
    public LayerMask circleLayer; //원판 감지
    public M3_Player_Stand playerStand;
    public M3_AimController aimController;

    private Rigidbody2D rb; //Rigidbody -> Rigidbody2D(7.29)
    private bool isGrounded = true;
    // Start is called before the first frame update
    void Start() => rb = GetComponent<Rigidbody2D>(); //RigidBody -> Rigidbody2D(7.29)

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            Vector3 dir = (aimController.transform.position - transform.position).normalized;
            rb.velocity = dir * jumpforce;

            playerStand.enabled = false; //회전 중단
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & circleLayer) != 0)
        {
            // 새 원판 착지
            playerStand.circle = col.transform;
            playerStand.enabled = true;

            aimController.circle = col.transform;

            //현재 원판 정보 계산
            FindObjectOfType<M3_MakeCircle>().SetCurrentPlayerCircle(col.gameObject);

            isGrounded = true;
        }
    }
}
