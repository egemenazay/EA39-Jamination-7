using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DoctorController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed;
    public float xTransformAmount = -0.6f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject wallCheck;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rb.velocity = new Vector2(speed , _rb.velocity.y);
        if (!IsCloseToEdge() || IsHitWall())
        {
            Flip();
            speed *= -1;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    private bool IsCloseToEdge()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, groundLayer);
    }
    
    private bool IsHitWall()
    {
        return Physics2D.OverlapCircle(wallCheck.transform.position, 0.2f, groundLayer);
    }
}
