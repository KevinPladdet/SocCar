using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlueCar : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float jumpBoost = 5.0f;
    private Rigidbody rb;
    private bool isJumping = false;
    private int jumpsRemaining = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isJumping && jumpsRemaining > 0)
        {
            rb.AddForce(Vector3.up * jumpBoost, ForceMode.Impulse);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            jumpsRemaining--;
            isJumping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        {
            isJumping = false;
            jumpsRemaining = 2;
        }
    }
}
