using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private int damage = 5;
    private Vector2 direction;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        transform.right = direction;
        transform.Rotate(0f, 0f, -45f);
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       Enemy enemy = collision.collider.GetComponent<Enemy>();

        if (collision.collider.CompareTag("Enemy"))
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);

        }
        
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public float GetSpeed() => speed;
}
