using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class tallcabinet : MonoBehaviour
{
    private float rotationSpeed = 30f; // 設定旋轉速度（度/秒）
    private float targetAngle = -90f; // 目標角度 90 度
    private bool trigger = false;

    private bool player1InContact = false; // 玩家1是否接觸物件
    private bool player2InContact = false; // 玩家2是否接觸物件
    private bool player1KeyPressed = false; // 玩家1是否按下空白鍵
    private bool player2KeyPressed = false; // 玩家2是否按下Enter鍵


    void Update()
    {   
        if (player1InContact && player1KeyPressed){         
            if (player2InContact && player2KeyPressed){
                if(trigger==false){
                    trigger = true;
                    StartCoroutine(RotateTo90Degrees());
                }
            }
        }
    }

    // 當玩家進入碰撞範圍
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cat"))
        {
            player1InContact = true;
            //Debug.Log("cAT");
        }
        else if (collision.collider.CompareTag("Dog"))
        {
            player2InContact = true;
            //Debug.Log("dog");
        }
    }

    // 當玩家離開碰撞範圍
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cat"))
        {
            player1InContact = false;
        }
        else if (collision.collider.CompareTag("Dog"))
        {
            player2InContact = false;
        }
    }


    
    public void dogInteraction(InputAction.CallbackContext context)
    {
        if (context.started)  // 當按下按鍵時
        {
            player2KeyPressed = true;
        }

        if (context.canceled)  // 當放開按鍵時
        {
            player2KeyPressed = false;
        }
    }
    public void catInteraction(InputAction.CallbackContext context)
    {
        if (context.started)  // 當按下按鍵時
        {
            player1KeyPressed = true;
        }

        if (context.canceled)  // 當放開按鍵時
        {
            player1KeyPressed = false;
        }
    }



    //旋轉櫃子
    IEnumerator RotateTo90Degrees()
    {
        float startAngle = transform.eulerAngles.z; // 取得當前的 Z 軸旋轉角度
        float currentAngle = 0f; // 設置相對旋轉角度

        while (currentAngle > targetAngle)
        {
            // 計算每一幀的旋轉量
            float angle = -1*rotationSpeed * Time.deltaTime;

            // 如果新角度超過 90 度，設置為 90 度，並停止旋轉
            if (currentAngle + angle < targetAngle)
            {
                angle = targetAngle - currentAngle;
            }

            // 更新物體旋轉
            transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);

            // 累加當前旋轉的角度
            currentAngle += angle;

            // 等待下一幀
            yield return null;
        }
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }
}