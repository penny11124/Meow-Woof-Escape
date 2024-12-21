using UnityEngine;

public class ImageSwing : MonoBehaviour
{
    public float swingAngle = 30f; // 最大擺動角度
    public float swingSpeed = 2.65f;  // 擺動速度

    private Quaternion initialRotation; // 保存物件的初始旋轉角度

    private void Start()
    {
        // 獲取物件的初始旋轉角度
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        // 計算偏移角度，使用 Mathf.Sin 確保平滑擺動
        float offsetAngle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        // 計算當前的旋轉角度
        Quaternion swingRotation = Quaternion.Euler(0, 0, offsetAngle);
        // 設置物件的旋轉角度，基於初始角度進行偏移
        transform.localRotation = initialRotation * swingRotation;
    }
}
