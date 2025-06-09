using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float fireRange = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletprefab;
    private float nextFireTime = 0.25f;
    private GameObject[] enemies;

    
    void Update()
    {
        GameObject enemy = FindEnemy();

        if (enemy != null)
        {
            if (Time.time >= nextFireTime)
            {
                Vector2 dir = (enemy.transform.position - firePoint.position).normalized;
                Bullet bulletClone = Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
                bulletClone.SetDirection(dir);
                //Rigidbody2D rb = bulletClone.GetComponent<Rigidbody2D>();
                ////Vector2 dir = (enemy.transform.position - firePoint.position).normalized;
                //rb.AddForce (dir * bulletClone.GetSpeed(), ForceMode2D.Impulse);
                nextFireTime = Time.time + fireRate;
            }
        }
    }


    public GameObject FindEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distanceFromPlayer = 0f;
        float minDistance = fireRange;
        GameObject closest = null;

        foreach (GameObject enemy in enemies)
        {
            distanceFromPlayer = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceFromPlayer <= minDistance)
            {
                minDistance = distanceFromPlayer;
                closest = enemy;
            }
        }
        return closest;
    }
}
