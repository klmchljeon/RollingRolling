using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_JumpControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    private bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
