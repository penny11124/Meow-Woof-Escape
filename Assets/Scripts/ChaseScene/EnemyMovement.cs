using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;  // Speed at which the enemy moves
    public float negativeMoveSpeed = -3f;  // Speed at which the enemy moves
    public GameObject cabinet;    // Reference to the cabinet (used to check if it’s knocked down)
    private bool isKnockedDown = false; 
    private Rigidbody2D rb;
    private float initialCabinetRotationZ;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (cabinet != null)
        {
            initialCabinetRotationZ = cabinet.transform.rotation.z;
        }
    }
    void Update()
    {
        // Check if the cabinet has changed position
        if (cabinet.transform.rotation.z != initialCabinetRotationZ)
        {
            isKnockedDown = true;
            moveSpeed = negativeMoveSpeed;
        }
        // If the cabinet is not knocked down, move the enemy left
        if (!isKnockedDown)
        {
            MoveEnemyToLeft();
        }
        else
        {
            MoveEnemyToLeft();
            spriteRenderer.flipX = false;
            //anim.enabled = false;
        }
    }

    // Move the enemy slowly to the left
    void MoveEnemyToLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

   
}
