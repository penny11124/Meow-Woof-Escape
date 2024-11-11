using UnityEngine;

public class PlayerCat : MonoBehaviour
{
    private Collider2D catCollider;
    private Collider2D dogMainCollider; // Dog 的主碰撞器
    private Collider2D dogTriggerCollider; // Dog 的觸發器碰撞器

    private void Start()
    {
        // 獲取 Cat 的 Collider
        catCollider = GetComponent<Collider2D>();

        // 初始化時忽略 Cat 和 Dog 之間的主碰撞器碰撞
        GameObject dog = GameObject.FindWithTag("Dog"); // 假設 Dog 標籤為 "Dog"
        if (dog != null)
        {
            dogMainCollider = dog.GetComponent<Collider2D>();
            if (dogMainCollider != null)
            {
                Physics2D.IgnoreCollision(catCollider, dogMainCollider, true);
            }

            // 確保我們獲取的是 Dog 的觸發器碰撞器
            Collider2D[] colliders = dog.GetComponents<Collider2D>();
            foreach (var col in colliders)
            {
                if (col.isTrigger)
                {
                    dogTriggerCollider = col;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == dogTriggerCollider)
        {
            // 只在接觸 Dog 的觸發器碰撞器時執行
            if (dogMainCollider != null)
            {
                // Debug.Log("打開 Cat 和 Dog 主碰撞器之間的碰撞");
                // 打開 Cat 和 Dog 主碰撞器之間的碰撞，使 Cat 可以附著在 Dog 上
                Physics2D.IgnoreCollision(catCollider, dogMainCollider, false);
                // transform.SetParent(dogMainCollider.transform); // 設置 Cat 為 Dog 的子物件
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == dogTriggerCollider)
        {
            // 只在離開 Dog 的觸發器碰撞器時執行
            if (dogMainCollider != null)
            {
                // Debug.Log("忽略忽略忽略忽略忽略");
                // 忽略 Cat 和 Dog 主碰撞器之間的碰撞
                Physics2D.IgnoreCollision(catCollider, dogMainCollider, true);
                // transform.SetParent(null); // 解除父子關係
            }
        }
    }
}
