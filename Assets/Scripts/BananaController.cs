using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float forwardPower;
    public float upPower;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (!PlayerController.isFacingRight)
        {
            forwardPower *= -1;
        }
        
        _rb.velocity = new Vector2(forwardPower, upPower);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
