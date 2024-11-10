using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack: MonoBehaviour
{
    public bool attackPressed = false;

    private void OnEnable()
    {
        // Subscribe to the Attack action
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Attack"].performed += OnAttackPressed;
        playerInput.actions["Attack"].canceled += OnAttackReleased;
    }

    private void OnDisable()
    {
        // Unsubscribe from the Attack action
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Attack"].performed -= OnAttackPressed;
        playerInput.actions["Attack"].canceled -= OnAttackReleased;
    }

    private void OnAttackPressed(InputAction.CallbackContext context)
    {
        attackPressed = true;
    }

    private void OnAttackReleased(InputAction.CallbackContext context)
    {
        attackPressed = false;
    }
}
