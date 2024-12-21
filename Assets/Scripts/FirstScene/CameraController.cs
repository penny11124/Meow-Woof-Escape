using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform object1; // 物件1
    public Transform object2; // 物件2

    public float leftLimit = -10f; // 攝影機的左邊界 (X 軸)
    public float rightLimit = 10f; // 攝影機的右邊界 (X 軸)
    public float bottomLimit = -0.47f; // 攝影機的底部邊界 (Y 軸)
    public float topLimit = 20f; // 攝影機的頂部邊界 (Y 軸)

    public float maxDistanceX = 20f; // X 軸的最大距離閾值
    public float maxDistanceY = 15f; // Y 軸的最大距離閾值
    public float normalOrthographicSize = 5.5f; // 攝影機的平常大小
    public float maxOrthographicSize = 20f; // 攝影機的最大大小
    public float zoomSpeed = 5f; // 攝影機縮放速度

    public float padding = 2f; // 給畫面增加的邊距 (單位：世界座標)

    void Update()
    {
        if (object1 != null && object2 != null)
        {
            // 計算物件1和物件2在 X 軸和 Y 軸的中間值
            float middleX = (object1.position.x + object2.position.x) / 2;
            float middleY = (object1.position.y + object2.position.y) / 2;

            // 根據攝影機的 bottomLimit 調整 Y 軸
            float orthographicHalfHeight = Camera.main.orthographicSize;
            float cameraBottom = middleY - orthographicHalfHeight;

            if (cameraBottom < bottomLimit)
            {
                middleY += bottomLimit - cameraBottom;
            }

            // 確保 X 軸和 Y 軸的中間值都在指定邊界內
            middleX = Mathf.Clamp(middleX, leftLimit, rightLimit);
            middleY = Mathf.Clamp(middleY, bottomLimit + orthographicHalfHeight, topLimit);

            // 設定攝影機的新位置，Z 軸保持不變
            transform.position = new Vector3(middleX, middleY, transform.position.z);

            // 計算兩物件之間的距離
            float distanceX = Mathf.Abs(object1.position.x - object2.position.x);
            float distanceY = Mathf.Abs(object1.position.y - object2.position.y);

            // 判斷是否需要放大
            bool needZoomX = distanceX > maxDistanceX;
            bool needZoomY = distanceY > maxDistanceY;

            float requiredSize = normalOrthographicSize;

            if (needZoomX || needZoomY)
            {
                // 根據 X 軸和 Y 軸分別計算需要的正交大小
                requiredSize = Mathf.Max(
                    distanceY / 2 + padding,
                    distanceX / Camera.main.aspect / 2 + padding
                );
            }

            // 限制正交大小範圍
            requiredSize = Mathf.Clamp(requiredSize, normalOrthographicSize, maxOrthographicSize);

            // 平滑過渡到目標大小
            Camera.main.orthographicSize = Mathf.Lerp(
                Camera.main.orthographicSize,
                requiredSize,
                Time.deltaTime * zoomSpeed
            );
        }
    }
}
