using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public float fireRate = 1f; 
    public float projectileSpeed = 10f; 
    public float maxDistance = 20f; 

    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; 
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = transform.forward * projectileSpeed; 
        }

        Destroy(projectile, maxDistance / projectileSpeed);
    }
}

