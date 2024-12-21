using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyTest : MonoBehaviour
{
    private bool catInContact = false; // 玩家1是否接觸物件
    private bool catKeyPressed = false; // 玩家1是否按下空白鍵
    // Start is called before the first frame update
    void Start()
    {
        // Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (catInContact && catKeyPressed)
        {
            Destroy(gameObject); // 銷毀當前物件
            Debug.Log("物件已被銷毀");
        }
        // else Debug.Log("NOTHING");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cat"))
        {
            catInContact = true;
            Debug.Log("Cat 接觸到物件");
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
    }
    
    public void catInteraction(InputAction.CallbackContext context)
    {
        if (context.started)  // 當按下按鍵時
        {
            catKeyPressed = true;
            // Destroy(gameObject);
            // Debug.Log("Cat 按下空白鍵");
        }

        if (context.canceled)  // 當放開按鍵時
        {
            catKeyPressed = false;
        }
    }
}
