using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;  // Speed at which the enemy moves
    public Transform leftBoundary; // Left boundary for patrol
    public Transform rightBoundary; // Right boundary for patrol

    private Rigidbody2D rb;
    private bool movingLeft = false; // Direction of movement

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Patrol logic
        Patrol();
    }

    private void Patrol()
    {
        if (movingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            if (transform.position.x <= leftBoundary.position.x)
            {
                movingLeft = false;
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            if (transform.position.x >= rightBoundary.position.x)
            {
                movingLeft = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Flip the sprite to face the new direction
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 改變方向
        movingLeft = !movingLeft;
        Flip();
    }
}
