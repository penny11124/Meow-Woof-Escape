using UnityEngine;

public class Clerk : MonoBehaviour
{
    public FirstTimer timerScript; // 引用計時器腳本
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Dog") || collision.gameObject.CompareTag("Cat"))
        {
            Debug.Log("Player碰到店員了!");
            if (timerScript != null)
            {
                timerScript.ModifyTime(-timerScript.timeRemaining);
            }
        }

        // 檢查是否與櫃子碰撞
        if (collision.gameObject.CompareTag("Cabinet"))
        {
            Debug.Log("店員被砸到了！");
            Destroy(gameObject); // 讓店員消失
        }
    }
}
