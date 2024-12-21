using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfantBehavior : MonoBehaviour
{
    public float movespeed = 2f; // 移動速度

    public string wallTag = "Wall"; // 用於標識牆壁的 Tag
    public string catchTagCat ;
    public string catchTagDog ;
    public Vector3 Cataddress;
    public Vector3 Dogaddress;
    public GameObject controlObject ;
    public GameObject babyImage1;
    public GameObject babyImage2;

    private bool player1InContact = false; // 玩家1是否接觸物件
    private bool player2InContact = false; // 玩家2是否接觸物件
    private bool player1KeyPressed = false; // 玩家1是否按下空白鍵
    private bool player2KeyPressed = false; // 玩家2是否按下Enter鍵
    private Stagecontrol controlstript;

    private Rigidbody2D rb; // Rigidbody2D 組件

    void Start()
    {
        // 獲取 Rigidbody2D 組件
        rb = GetComponent<Rigidbody2D>();
        controlstript = controlObject.GetComponent<Stagecontrol>();
    }

    void Update()
    {
        // 控制角色的移動
        rb.velocity = new Vector2(movespeed, rb.velocity.y);

        if (player1InContact && player1KeyPressed){
            if (player2InContact && player2KeyPressed){
                controlstript.trueEnd = true;
                babyImage1.SetActive(true);
                babyImage2.SetActive(true);
                Destroy(gameObject);
            }
        }        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢測是否碰到牆壁
        if (collision.gameObject.CompareTag(wallTag))
        {
            // 碰到牆壁時，改變移動方向
            Flip();
        }
        if (collision.gameObject.CompareTag(catchTagCat))
        {
            player1InContact = true;
            if(controlstript.getBear == false){
               // 將碰到的物件移動到特定座標 (x, y)
                Vector3 targetPosition = Cataddress;
                collision.gameObject.transform.position = targetPosition; 
            }
            else{
                Flip();
            }
            
        }
        if (collision.gameObject.CompareTag(catchTagDog))
        {
            player2InContact = true;
            if(controlstript.getNipple == false){
                // 將碰到的物件移動到特定座標 (x, y)
                Vector3 targetPosition = Dogaddress;
                collision.gameObject.transform.position = targetPosition;
            }
            else{
                Flip();
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(catchTagCat))
        {
            player1InContact = false;
        }
        if (collision.gameObject.CompareTag(catchTagDog))
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


    void Flip()
    {
        movespeed *= -1;

        // 翻轉角色的 x 軸方向
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
