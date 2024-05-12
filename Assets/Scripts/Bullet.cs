using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed;
    public Transform soliderRotate;
    public int bulletCode;

    private void Start()
    {
        if (bulletCode == 1)
        {
            Flip();
        }
    }

    void Update()
    {
        if (soliderRotate.localScale.x <0)
        {
            transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
        }
    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
