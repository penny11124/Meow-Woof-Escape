using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhiteDoorControl : MonoBehaviour
{
    // Start is called before the first frame update
    private bool player1InContact = false; // 玩家1是否接觸物件
    private bool player1KeyPressed = false; // 玩家1是否按下鍵

    public bool canopen = false;             //是否持有鑰匙
    public string charTag ;
    public GameObject targetObject;
    public Vector3 address;
    private DoorScript.Door doorScript;


    void Start()
    {
        
    }

    void Update()
    {   
        if (player1InContact && player1KeyPressed){
            if(canopen){
                Opendoor();
                Invoke("Transport", 0.5f);
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

    void Opendoor(){
        //開門動畫
        Transform firstChild = transform.GetChild(0);
        if (firstChild != null){
            doorScript = firstChild.GetComponent<DoorScript.Door>();
            if (doorScript != null){
                doorScript.open = true;
                Invoke("CloseDoor", 3f);
            }
        }  
    }
    void CloseDoor()
    {
        doorScript.open = false;
    }

    void Transport()
    {
        targetObject.transform.position = address;
    }
}
