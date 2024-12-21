using UnityEngine;

public class CameraFollowBetweenObjects : MonoBehaviour
{
    public Transform object1; // 物件1
    public Transform object2; // 物件2
    public float yshift = 4f;
    public float ymax = 1.27f;

    void Update()
    {
        if (object1 != null && object2 != null)
        {
            // 計算物件1和物件2在 X 軸的中間值
            float middleX = (object1.position.x + object2.position.x) / 2;
            float middleY = (object1.position.y + object2.position.y) / 2;

            // 設定攝影機的新位置，Y 和 Z 軸保持不變
            transform.position = new Vector3(middleX, Mathf.Min(middleY+yshift,ymax), transform.position.z);
        }
    }
}
