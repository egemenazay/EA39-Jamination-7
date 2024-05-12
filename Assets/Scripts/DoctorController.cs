using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DoctorController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed;
    public float health = 5f;
    [SerializeField] private SimpleFlash _flashEffect;
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

        if (health<0)
        {
            Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Flip();
        }

        if (other.gameObject.CompareTag("Banana"))
        {
            _flashEffect.Flash();
            health--;
        }
    }
}
