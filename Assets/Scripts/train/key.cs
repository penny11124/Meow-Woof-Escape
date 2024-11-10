using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class key : MonoBehaviour
{
    // Start is called before the first frame update    
    private bool player1InContact = false; // 玩家1是否接觸物件
    private bool player2InContact = false; // 玩家2是否接觸物件
    private bool player1KeyPressed = false; // 玩家1是否按下空白鍵
    private bool player2KeyPressed = false; // 玩家2是否按下Enter鍵
    private bool trigger = false;
    public GameObject door;
    void Start()
    {
        
    }

    void Update()
    {   
        if (player1InContact && player1KeyPressed){
            if(trigger==false){
                trigger = true;
                Keytake();
            }
        }         
        else if (player2InContact && player2KeyPressed){
            if(trigger==false){
                trigger = true;
                Keytake();
            }
        } 
    }

    // 當玩家進入碰撞範圍
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            player1InContact = true;
        }
        else if (other.CompareTag("Dog"))
        {
            player2InContact = true;
        }
    }
    // 當玩家離開碰撞範圍
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            player1InContact = false;
        }
        else if (other.CompareTag("Dog"))
        {
            player2InContact = false;
        }
    }

    //newinput按鍵
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
    void Keytake(){
        this.enabled = false;
        gameObject.SetActive(false);
        if (door != null)
        {
            doorcontrol doorScript = door.GetComponent<doorcontrol>();
            if (doorScript != null)
            {
                doorScript.hadkey = true; 
            }
        }
    }

}
