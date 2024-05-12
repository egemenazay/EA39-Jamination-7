using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    private float _horizontal;
    public float speed = 8f;
    public float jumpPower = 16f;
    public static bool isFacingRight = true;
    private bool isAlive = true;
    public int jumpCount;
    public int jumpAmount;
    public GameObject banana;
    public Animator animator;
    public GameObject deathScreen;
    public GameObject cage;
    public GameObject boss;
    public GameObject popUp;
    //Health System Vars
    public int health;
    public int numOfhearts;
    public int nextBuildIndex = 2;
    public int currentBuildIndex;
    public Image[] hearts;
    public Sprite fullHearth;
    public Sprite emptyHearth;
    
    
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
        if (isAlive)
        {
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
        //Health UI System
        if (health > numOfhearts)
        {
            health = numOfhearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHearth;
            }
            else
            {
                hearts[i].sprite = emptyHearth;
            }
            if (i<numOfhearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health<1)
        {
            animator.SetTrigger("isDead");
            isAlive = false;
            Invoke("StopGameTime",2f);
        }

        if (!boss.activeSelf)
        {
            popUp.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                cage.SetActive(false);
                Invoke("LoadNewScene",2f);
            }
        }
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene(nextBuildIndex);
    }
    private void StopGameTime()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Enemy"))
        {
            health--;
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            health = 0;
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            rb.velocity = new Vector2(_horizontal * speed, rb.velocity.y);
        }
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
