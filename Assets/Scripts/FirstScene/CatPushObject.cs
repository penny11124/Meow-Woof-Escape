using UnityEngine;
using UnityEngine.InputSystem;

public class CatPushObject : MonoBehaviour
{
    public float pushForce = 10f; // 推動力的大小
    private GameObject pushableObject; // 用來儲存當前可推動的物件

    private bool isNearCabinet = false; // 是否靠近櫃子
    private Rigidbody2D cabinetRb; // 櫃子的 Rigidbody2D

    // Input System 的空白鍵事件
    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.started && isNearCabinet && cabinetRb != null)
        {
            // 當按下空白鍵並且靠近櫃子時，施加水平推動力
            Vector2 pushDirection = new Vector2(transform.localScale.x, 0).normalized; // 根據角色面向方向推動
            cabinetRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            // Debug.Log("推動櫃子！");
        }
    }

    // 偵測角色是否靠近櫃子
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cabinet")) // 櫃子需加 "Cabinet" Tag
        {
            isNearCabinet = true;
            cabinetRb = collision.gameObject.GetComponent<Rigidbody2D>();
            // Debug.Log("靠近櫃子，可以推動");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cabinet"))
        {
            isNearCabinet = false;
            cabinetRb = null;
            // Debug.Log("離開櫃子");
        }
    }
}
