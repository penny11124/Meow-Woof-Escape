using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftButtonControl : MonoBehaviour
{
    private bool player1InContact = false; // 玩家1是否接觸物件
    private bool player1KeyPressed = false; // 玩家1是否按下鍵

    public bool canopen = false;             //是否持有鑰匙
    public string charTag ;
    public GameObject targetObject ;
    public float rotationDuration = 2f;
 


    void Start()
    {
        
    }

    void Update()
    {   
        if (player1InContact && player1KeyPressed){
            if(canopen){
                Vector3 currentScale = transform.localScale;
                currentScale.y *= 0.5f;
                transform.localScale = currentScale;
                RemoveClerk();
                canopen = false;
            }
        }
    }

    // 當玩家進入碰撞範圍
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(charTag))
        {
            player1InContact = true;
        }
    }
    // 當玩家離開碰撞範圍
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(charTag))
        {
            player1InContact = false;
        }
    }

    //newinput按鍵

    public void charInteraction(InputAction.CallbackContext context)
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

    void RemoveClerk(){
        //停止店員
        if (targetObject != null){
            ShopkeeperBehavior shopkeeper = targetObject.GetComponent<ShopkeeperBehavior>();
            if (shopkeeper != null)
            {
                shopkeeper.enabled = false; // 禁用脚本
            }
            Collider2D collider = targetObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            Rigidbody2D rb = targetObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true; // 设置为 kinematic，禁用物理作用
                rb.velocity = Vector2.zero; // 清空速度
            }
            Animator ani = targetObject.GetComponent<Animator>();
            if (ani != null)
            {
                ani.enabled = false; // 暂停 Animator 控制
            }
            SpriteRenderer renderer = targetObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                // 确保该对象有独立的材质
                renderer.material = new Material(renderer.material);

                // 修改颜色的透明度
                Color color = renderer.color;
                color.a *= 0.5f; // 将透明度减半
                renderer.color = color;
            }
            else
            {
                Debug.LogWarning("目标对象缺少 SpriteRenderer 组件！");
            }
        }  
    }


}