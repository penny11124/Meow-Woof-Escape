using UnityEngine;

public class Clerk : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查是否與櫃子碰撞
        if (collision.gameObject.CompareTag("Cabinet"))
        {
            Debug.Log("店員被砸到了！");
            Destroy(gameObject); // 讓店員消失
        }
    }
}
