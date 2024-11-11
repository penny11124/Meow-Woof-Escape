using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack: MonoBehaviour
{
    public bool isAttacking { get; private set; } = false;
    public bool attackPressed = false;

    private void OnEnable()
    {
        // Subscribe to the Attack action
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["interaction"].performed += OnAttackPressed;
        playerInput.actions["interaction"].canceled += OnAttackReleased;
    }

    private void OnDisable()
    {
        // Unsubscribe from the Attack action
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["interaction"].performed -= OnAttackPressed;
        playerInput.actions["interaction"].canceled -= OnAttackReleased;
    }

    private void OnAttackPressed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        isAttacking = true;
        attackPressed = true;
    }

    private void OnAttackReleased(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        isAttacking = false;
        attackPressed = false;
    }
}
