using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorRotate : MonoBehaviour
{
    public float rotationSpeed = 20f; // 每秒旋转速度，单位为度数
    public float rotationAngle = 60f;
    private float initialAngle;      // 记录物体初始角度

    void Start()
    {
        initialAngle = transform.eulerAngles.z; // 记录物体 Z 轴上的初始角度
        StartCoroutine(RotateBackAndForth());
    }

    IEnumerator RotateBackAndForth()
    {
        while (true)
        {
            // 顺时针旋转 90 度（基于初始角度）
            yield return RotateToAngle(initialAngle - rotationAngle);
            // 等待 0.5 秒
            yield return new WaitForSeconds(0.5f);
            // 逆时针旋转回初始角度
            yield return RotateToAngle(initialAngle);
            // 等待 0.5 秒
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RotateToAngle(float targetAngle)
    {
        float currentAngle = NormalizeAngle(transform.eulerAngles.z); // 当前 Z 轴角度
        targetAngle = NormalizeAngle(targetAngle); // 目标角度归一化

        // 计算短路径的旋转方向和角度差
        float angleDifference = targetAngle - currentAngle;
        if (Mathf.Abs(angleDifference) > 180f)
        {
            angleDifference = -Mathf.Sign(angleDifference) * (360f - Mathf.Abs(angleDifference));
        }

        float totalTime = Mathf.Abs(angleDifference) / rotationSpeed; // 计算所需总时间
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.Lerp(currentAngle, currentAngle + angleDifference, elapsedTime / totalTime);
            transform.eulerAngles = new Vector3(0, 0, NormalizeAngle(angle)); // 更新 Z 轴旋转
            yield return null; // 等待下一帧
        }

        // 确保最终角度精确到目标角度
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }


    float NormalizeAngle(float angle)
    {
        return (angle % 360f + 360f) % 360f; // 将角度规范化到 [0, 360) 范围内
    }
}