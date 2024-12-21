using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightButtonControl : MonoBehaviour
{
    private bool player1InContact = false; // 玩家1是否接觸物件
    private bool player1KeyPressed = false; // 玩家1是否按下鍵

    public bool canopen = false;             //是否持有鑰匙
    public string charTag ;
    public GameObject targetObject ;

 


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
                BanMonitor();
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

    void BanMonitor(){
        //停止店員
        if (targetObject != null){
            MonitorControllerSecondStage monitor = targetObject.GetComponent<MonitorControllerSecondStage>();
            if (monitor != null)
            {
                monitor.enabled = false; // 禁用脚本
            }
            if (targetObject.transform.childCount >= 2)
            {
                Transform secondChild = targetObject.transform.GetChild(1); // 获取第二个子对象
                Destroy(secondChild.gameObject);
            }  
        }
    }


}