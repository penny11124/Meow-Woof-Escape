using UnityEngine;

public class ObjectDropper : MonoBehaviour
{
    public Transform[] objectsToDrop; // 上半部分的物體
    public Transform cat;             // 貓的 Transform
    public Transform dog;             // 狗的 Transform
    public float triggerDistance = 6f; // 觸發距離

    public FirstTimer timerScript; // 引用計時器腳本

    private bool[] hasDropped; // 記錄物體是否已經掉落

    private void Start()
    {
        if (objectsToDrop == null || objectsToDrop.Length == 0)
        {
            Debug.LogError("ObjectsToDrop 未正確分配，請在 Inspector 中設置物件。");
            return;
        }

        hasDropped = new bool[objectsToDrop.Length];
    }

    private void Update()
    {
        for (int i = 0; i < objectsToDrop.Length; i++)
        {
            if (objectsToDrop[i] == null || hasDropped[i])
                continue;

            Transform obj = objectsToDrop[i];
            if (IsNear(cat, obj) || IsNear(dog, obj))
            {
                Debug.Log("Drop!");
                DropObject(obj);
                hasDropped[i] = true;
            }
        }
    }

    private bool IsNear(Transform target, Transform obj)
    {
        return Vector2.Distance(target.position, obj.position) <= triggerDistance;
    }

    private void DropObject(Transform obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obj.gameObject.AddComponent<Rigidbody2D>();
        }

        if (rb != null)
        {
            rb.gravityScale = 1; // 啟用重力
            rb.simulated = true;

            // 添加碰撞檢測腳本到掉落物上
            obj.gameObject.AddComponent<ObjectCollisionHandler>().Initialize(timerScript);

            Debug.Log($"{obj.name} 已掉落！");
        }
        else
        {
            Debug.LogError($"{obj.name} 無法添加 Rigidbody2D！");
        }
    }
}

// 處理物品碰撞邏輯
public class ObjectCollisionHandler : MonoBehaviour
{
    private FirstTimer timerScript;

    public void Initialize(FirstTimer timer)
    {
        timerScript = timer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cat") || collision.collider.CompareTag("Dog"))
        {
            Debug.Log($"{gameObject.name} 砸中 {collision.collider.name}!");

            // 扣除 10 秒時間
            if (timerScript != null)
            {
                timerScript.ModifyTime(-10f);
            }

            // 物品消失
            Destroy(gameObject);
        }
    }
}
