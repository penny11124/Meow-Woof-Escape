using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperBehavior : MonoBehaviour
{
    public float movespeed = 2f; // 移動速度

    public string wallTag = "Wall"; // 用於標識牆壁的 Tag
    public string catchTag ; // 用於標識牆壁的 Tag
    public Vector3 address;

    private Rigidbody2D rb; // Rigidbody2D 組件

    void Start()
    {
        // 獲取 Rigidbody2D 組件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 控制角色的移動
        rb.velocity = new Vector2(movespeed, rb.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢測是否碰到牆壁
        if (collision.gameObject.CompareTag(wallTag))
        {
            // 碰到牆壁時，改變移動方向
            Flip();
        }
        if (collision.gameObject.CompareTag(catchTag))
        {
            // 將碰到的物件移動到特定座標 (x, y)
            Vector3 targetPosition = address;
            collision.gameObject.transform.position = targetPosition;
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
