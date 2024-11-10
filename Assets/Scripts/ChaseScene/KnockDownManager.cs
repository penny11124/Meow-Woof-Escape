using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownManager : MonoBehaviour
{
    public PlayerAttack playerA;
    public PlayerAttack playerB;
    public GameObject cabinet;  // The cabinet game object to knock down

    private bool knockdownTriggered = false;

    void Update()
    {
        if (!knockdownTriggered && playerA.attackPressed && playerB.attackPressed)
        {
            TriggerKnockdown();
        }
    }

    void TriggerKnockdown()
    {
        knockdownTriggered = true;

        // Use Rigidbody2D to interact with 2D physics
        Rigidbody2D cabinetRb = cabinet.GetComponent<Rigidbody2D>();
        if (cabinetRb != null)
        {
            cabinetRb.isKinematic = false;
            Vector2 topOfCabinet = new Vector2(transform.position.x, transform.position.y + 1f);
            cabinetRb.AddForceAtPosition(4f * new Vector2(0, 1), topOfCabinet, ForceMode2D.Impulse);
        }

        Debug.Log("Cabinet knocked down!");
    }
}
