using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyObject : MonoBehaviour
{
    private bool catInContact = false; // 玩家1是否接觸物件
    private bool dogInContact = false; // 玩家2是否接觸物件
    private bool catKeyPressed = false; // 玩家1是否按下空白鍵
    private bool dogKeyPressed = false; // 玩家2是否按下Enter鍵

    void Update()
    {
        if ((catInContact && catKeyPressed) || (dogInContact && dogKeyPressed))
        {
            Destroy(gameObject); // 銷毀當前物件
            // Debug.Log("物件已被銷毀");
        }
    }

    // 當玩家進入碰撞範圍
    void OnCollisionEnter2D(Collision2D collision)
    {
         Debug.Log($"碰撞的對象是：{collision.collider.tag}");
        if (collision.collider.CompareTag("Cat"))
        {
            catInContact = true;
             Debug.Log("Cat 接觸到物件");
        }
        else if (collision.collider.CompareTag("Dog"))
        {
            dogInContact = true;
             Debug.Log("Dog 接觸到物件");
        }
    }

    // 當玩家離開碰撞範圍
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cat"))
        {
            catInContact = false;
             Debug.Log("Cat 離開物件");
        }
        else if (collision.collider.CompareTag("Dog"))
        {
            dogInContact = false;
             Debug.Log("Dog 離開物件");
        }
    }

    public void catInteraction(InputAction.CallbackContext context)
    {
        if (context.started)  // 當按下按鍵時
        {
            catKeyPressed = true;
             Debug.Log("Cat 按下空白鍵");
        }

        if (context.canceled)  // 當放開按鍵時
        {
            catKeyPressed = false;
        }
    }

    public void dogInteraction(InputAction.CallbackContext context)
    {
        if (context.started)  // 當按下按鍵時
        {
            dogKeyPressed = true;
             Debug.Log("Dog 按下 Enter 鍵");
        }

        if (context.canceled)  // 當放開按鍵時
        {
            dogKeyPressed = false;
        }
    }
}
