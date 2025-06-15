using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int enemyDmg = 5; // Danno che l'enemy infligge al player
    [SerializeField] private int _enemyHealth = 10; // Vita dell'enemy
    private PlayerController playerController;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("Player"))
        {
            
            LifeController playerLifeController = collision.collider.GetComponent<LifeController>();

            if (playerLifeController != null)
            {
                playerLifeController.TakeDamage(enemyDmg); 
                Debug.Log($"Il nemico ha colpito il giocatore, infliggendo {enemyDmg} danno.");
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        EnemyMovement();
    }

    private void Awake()
    {
        playerController = Object.FindAnyObjectByType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    public void EnemyMovement()
    {
        if (playerController != null)
        {
            Vector2 targetPosition = playerController.transform.position;
            moveDirection = (targetPosition - rb.position).normalized;
        }
    }

    
    public void TakeDamage(int damageAmount)
    {
        _enemyHealth -= damageAmount;
        Debug.Log($"Nemico ha subito {damageAmount} danni. Salute rimanente: {_enemyHealth}");

        if (_enemyHealth <= 0)
        {
            Debug.Log("Nemico sconfitto!");
            Destroy(gameObject); // Distrugge l'Enemy quando la sua salute arriva a zero
        }
    }

}
