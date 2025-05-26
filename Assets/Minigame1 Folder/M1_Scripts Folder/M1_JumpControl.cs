using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_JumpControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce = 7f;

    public GroundChecker groundChecker;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (groundChecker != null && groundChecker.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
    }


    void Update()
    {
        
    }
}

    // Update is called once per frame

