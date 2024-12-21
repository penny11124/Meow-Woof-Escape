using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform object1; // 物件1
    public Transform object2; // 物件2

    public float leftLimit = -122.44f; // 攝影機最左邊的位置
    public float rightLimit = 87.76f; // 攝影機最右邊的位置

    void Update()
    {
        if (object1 != null && object2 != null)
        {
            // 計算物件1和物件2在 X 軸的中間值
            float middleX = (object1.position.x + object2.position.x) / 2;

            middleX = Mathf.Clamp(middleX, leftLimit, rightLimit);

            // 設定攝影機的新位置，Y 和 Z 軸保持不變
            transform.position = new Vector3(middleX, transform.position.y, transform.position.z);
        }
    }
}
