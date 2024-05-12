using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bileşeni
    public GameObject bulletPrefab; // Mermi prefabı
    public Transform firePoint; // Mermi ateşleme noktası
    public float fireRate = 1f;
    public float health = 5f;
    [SerializeField] private SimpleFlash _flashEffect;// Ateş hızı (saniyede bir atış)
    private float nextFireTime = 0f; // Bir sonraki ateş zamanı

    public float detectionRange = 55f; // Düşmanın algılama menzili

    void Update()
    {
        // Oyuncu düşmanın algılama menziline girerse
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Ateş hızı kontrolü
            if (Time.time >= nextFireTime)
            {
                // Mermi ateşleme fonksiyonunu çağır
                Shoot();
                // Bir sonraki ateş zamanını belirle
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        if (health<0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Banana"))
        {
            health--;
            _flashEffect.Flash();
        }
    }

    void Shoot()
    {
        Debug.Log("mermi ateşlendi");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
