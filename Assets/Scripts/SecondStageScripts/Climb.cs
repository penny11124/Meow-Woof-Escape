using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Climb : MonoBehaviour
{
    public float climbSpeed = 3f; // 攀爬速度
    public string climbableTag = "Climbable"; // 可攀爬物體的 Tag

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    public bool isClimbing = false; // 是否在梯子上

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (isClimbing)
        {
            // 禁用重力
            rb.gravityScale = 0;
        }
        else
        {
            // 恢復重力
            rb.gravityScale = 1;
        }
    }
    public void climb(InputAction.CallbackContext context)
    {
        if (isClimbing) // 只有在攀爬狀態下處理輸入
        {
            Vector2 climbDirection = Vector2.zero;

            if (context.started || context.performed)
            {
                Vector2 input = context.ReadValue<Vector2>();
                climbDirection = new Vector2(0, input.y); // 獲取垂直方向
            }

            if (context.canceled)
            {
                climbDirection = Vector2.zero; // 停止移動
            }

            rb.velocity = new Vector2(rb.velocity.x, climbDirection.y * climbSpeed); // 設置垂直速度
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 離開可攀爬物體，退出攀爬模式
        if (other.CompareTag(climbableTag))
        {
            EnterClimbMode();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(climbableTag))
        {
            ExitClimbMode();
        }
    }


    void EnterClimbMode()
    {
        isClimbing = true;

        // 暫時禁用跳躍輸入
        playerInput.actions["Jump"].Disable();
    }

    void ExitClimbMode()
    {
        isClimbing = false;

        // 恢復跳躍輸入
        playerInput.actions["Jump"].Enable();
    }
}

