using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _horizontal;
    public float speed = 8f;
    public float jumpPower = 16f;
    public static bool isFacingRight = true;
    public int jumpCount;
    public int jumpAmount;
    public GameObject banana;
    public Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        jumpCount = jumpAmount;
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        if ((Input.GetKeyDown(KeyCode.W) && IsGrounded()) || (Input.GetKeyDown(KeyCode.W) && !IsGrounded() && jumpCount>0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount--;
        }
        if (IsGrounded())
        {
            animator.SetBool("onAir",false);
            jumpCount = jumpAmount;
        }

        if (!IsGrounded())
        {
            animator.SetBool("onAir",true);
        }
        Flip();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBanana();
        }
        if (_horizontal !=0)
        {
            animator.SetBool("isMoving",true);
        }
        else
        {
            animator.SetBool("isMoving",false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && _horizontal<0f || !isFacingRight && _horizontal>0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        
    }

    private void LaunchBanana()
    {
        Vector3 bananaPos = new Vector3(transform.position.x + 0.1f, transform.position.y + 0.8f, transform.position.z);
        Instantiate(banana, bananaPos, transform.rotation);
    }
    
}
