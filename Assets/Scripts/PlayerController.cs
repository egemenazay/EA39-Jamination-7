using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _plyrRigidbody2D;
    public float speed;
    public float jumpForceValue;
    public int jumpAmount = 2;
    private int jumpCount;
    public GameObject ayak;
    private bool isGround = true;
    private void Start()
    {
        _plyrRigidbody2D = GetComponent<Rigidbody2D>();
        jumpCount = jumpAmount;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _plyrRigidbody2D.velocity = new Vector2(horizontalInput * speed, _plyrRigidbody2D.velocity.y);
        if ((Input.GetKeyDown(KeyCode.W) && isGround == true) || ((Input.GetKeyDown(KeyCode.W) && isGround == false && jumpCount >0)))
        {
            _plyrRigidbody2D.AddForce(Vector2.up * jumpForceValue, ForceMode2D.Impulse);
            isGround = false;
            jumpCount--;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Hit ground");
            isGround = true;
            jumpCount = jumpAmount;
        }
    }

}
