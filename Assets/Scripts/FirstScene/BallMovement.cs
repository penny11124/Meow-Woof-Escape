using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f; // 滾動的速度
    public float triggerXPosition; // 球開始滾動的 x 位置
    public Transform cat;             // 貓的 Transform
    public Transform dog;             // 狗的 Transform
    private Rigidbody2D rb;
    private bool isRolling = false; // 標記球是否已經開始滾動

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; // 初始靜止
    }

    private void Update()
    {
        // 如果球開始滾動，計算旋轉角度模擬滾動效果
        if (isRolling)
        {
            float rotationAngle = rb.velocity.x * Time.deltaTime * -360f / (2 * Mathf.PI * transform.localScale.x / 2);
            transform.Rotate(0, 0, rotationAngle);
        } else if (cat.transform.position.x > triggerXPosition || dog.transform.position.x > triggerXPosition)
        {
            Debug.Log($"超過了觸發點，球開始滾動！");
            StartRolling();
        }
    }

    private void StartRolling()
    {
        if (!isRolling) // 防止重複啟動滾動
        {
            isRolling = true;
            rb.velocity = new Vector2(-speed, rb.velocity.y); // 設置向左滾動的速度
        }
    }
}
