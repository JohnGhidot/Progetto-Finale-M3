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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Obstacle"))
        {
            //if da aggiungere col danno
            //if (collision.collider.CompareTag("Enemy"))
            //{
            //    collision.collider.GetComponent<Enemy>().TakeDamage();
            //}

            Destroy(collision.gameObject);
            Destroy(gameObject);



        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public float GetSpeed() => speed;
}
