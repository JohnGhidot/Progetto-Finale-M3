using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float fireRange = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform _gunEquipPoint;
    [SerializeField] private Bullet bulletprefab;
    private float nextFireTime = 0.25f;
    private GameObject[] enemies;

    private bool _isEquipped = false;

    private SpriteRenderer gunSpriteRenderer;
    private PlayerController playerController;

    void Awake()
    {
        gunSpriteRenderer = GetComponent<SpriteRenderer>();        
    }

    public void SetPlayerReference(PlayerController player) //giro subito l'arma appena la equipaggio
    {
        playerController = player;
        ApplyWeaponFlipping();
    }

    void Update()
    {
               

        if (!_isEquipped)
        {
            return; // Se non è equipaggiata, non fare nulla
        }

        if (playerController != null) //segue così sempre la direzione del player
        {
            ApplyWeaponFlipping();
        }

        GameObject enemy = FindEnemy();
        
        if (enemy != null)
        {
            float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);
        }

       
        if (enemy != null)
        {
            if (Time.time >= nextFireTime)
            {
               
                if (firePoint == null)
                {                  
                    return; 
                }
                if (bulletprefab == null)
                {                    
                    return; 
                }

                Vector2 dir = (enemy.transform.position - firePoint.position).normalized;
                Bullet bulletClone = Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
                bulletClone.SetDirection(dir);                
                nextFireTime = Time.time + fireRate;
            }
        }
    }
    public void SetEquippedStatus(bool status)
    {
        _isEquipped = status;
        gameObject.SetActive(status);
    }

    public GameObject FindEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distanceFromPlayer = 0f;
        float minDistance = fireRange;
        GameObject closest = null;

        if (enemies == null || enemies.Length == 0)
        {            
            return null; // Nessun nemico trovato
        }

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

    private void ApplyWeaponFlipping()
    {
        if (playerController == null || gunSpriteRenderer == null) return;

        bool playerIsFacingRight = playerController._isFacingRight;
        gunSpriteRenderer.flipX = !playerIsFacingRight;
        //transform.localPosition = Vector3.zero;
    }

}
