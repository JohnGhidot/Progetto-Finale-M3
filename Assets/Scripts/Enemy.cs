using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private PlayerController playerController;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void Awake()
    {
        playerController = Object.FindObjectOfType<PlayerController>();
    }

    public void EnemyMovement()
    {
        float Direction = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerController.transform.position, Direction);
    }

}
