using UnityEngine;

public class CatPushObject : MonoBehaviour
{
    public float pushForce = 10f; // 推動力的大小
    private Rigidbody2D cabinetRb; // 儲存櫃子的 Rigidbody2D

    private void FixedUpdate()
    {
        // 如果櫃子存在，持續施加推動力
        if (cabinetRb != null)
        {
            Vector2 pushDirection = new Vector2(transform.localScale.x, 0).normalized; // 根據角色面向方向推動
            cabinetRb.AddForce(pushDirection * pushForce, ForceMode2D.Force); // 持續施加推動力
        }
    }

    // 偵測角色是否與櫃子碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cabinet")) // 櫃子需加 "Cabinet" Tag
        {
            cabinetRb = collision.gameObject.GetComponent<Rigidbody2D>(); // 儲存櫃子的剛體
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cabinet"))
        {
            cabinetRb = null; // 離開櫃子時停止施加力
        }
    }
}
