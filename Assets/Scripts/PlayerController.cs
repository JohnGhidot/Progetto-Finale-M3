using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private float h;
    private float v;
    Vector2 Direction;

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Direction = new Vector2(h, v);
        rb.MovePosition(rb.position + Direction *(speed * Time.deltaTime));
    }
}
