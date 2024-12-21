using UnityEngine;

public class Cabinet : MonoBehaviour
{
    public bool isKnockedDown = false; // 標記櫃子是否被推倒

    public int affectedLayerIndex; // 直接指定影響的圖層索引（0~31 範圍內的整數）

    private void Start()
    {

        // 確保圖層索引有效（0~31）
        if (affectedLayerIndex < 0 || affectedLayerIndex > 31)
        {
            Debug.LogError("affectedLayerIndex 超出範圍，請確認設置！");
            return;
        }

        Physics2D.IgnoreLayerCollision(gameObject.layer, affectedLayerIndex, true);
    }

    private void Update()
    {
        if (transform.rotation.eulerAngles.z > 260 && transform.rotation.eulerAngles.z < 315 && !isKnockedDown)
        {
            isKnockedDown = true;
            Debug.Log("櫃子被推倒了！");

            Physics2D.IgnoreLayerCollision(gameObject.layer, affectedLayerIndex, false);
        }
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Clerk"))
        {
            Destroy(gameObject);
        }
    }
}
