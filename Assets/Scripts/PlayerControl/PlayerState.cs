using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerState : MonoBehaviour
{
    private bool winState = false;
    // public bool hasWon => winState;
    private PlayerInput playerInput;
    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }
    public void Win() {
        winState = true;
        playerInput.DeactivateInput();
        // TODO: Restart scene (maybe fade?)
    }
}
