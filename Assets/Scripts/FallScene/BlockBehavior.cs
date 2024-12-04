using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private float randomNum;
    private float randomDir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            randomNum = Random.Range(0f, 200f);
            randomDir = Random.Range(0, 2) == 0 ? -1 : 1;
            rb.angularVelocity = randomNum * randomDir; // Set the angular velocity for spinning
            rb.gravityScale = 0.5f * randomNum / 200f + 0.3f;
        }
    }
}
