using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;  // Speed at which the enemy moves
    public GameObject cabinet;    // Reference to the cabinet (used to check if it’s knocked down)
    private bool isKnockedDown = false; // Flag to track if the cabinet is knocked down
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Check if the cabinet is knocked down
        CheckCabinetStatus();

        // If the cabinet is not knocked down, move the enemy left
        if (!isKnockedDown)
        {
            MoveEnemyToLeft();
        }
    }

    // Move the enemy slowly to the left
    void MoveEnemyToLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    // Check if the cabinet is knocked down (this is just a placeholder condition)
    void CheckCabinetStatus()
    {
        {
            Rigidbody2D cabinetRb = cabinet.GetComponent<Rigidbody2D>();
            if (cabinetRb != null && !cabinetRb.isKinematic)  // Assuming knocked down means isKinematic is false
            {
                isKnockedDown = true;
                Debug.Log("Cabinet knocked down, enemy stops!");
            }
        }
    }
}
