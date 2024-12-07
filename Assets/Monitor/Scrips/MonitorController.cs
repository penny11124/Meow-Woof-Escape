using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    public float detectionAngle = 30f; // 扇形的角度
    public float detectionRadius = 10f; // 檢測的半徑
    public float detectionDelay = 1f; // 開始檢測的延遲時間
    public Color detectionColor = new Color(1f, 0f, 0f, 0.3f); // 扇形顏色
    public LayerMask detectionLayer; // 偵測圖層 (Cat 和 Dog)
    public float detectionAngleOffset = 270f; // 偵測範圍的旋轉角度

    private Mesh detectionMesh;
    private Material detectionMaterial;
    private bool isDetecting = false;

    void Start()
    {
        // 初始化 Mesh 和材質
        detectionMesh = new Mesh();
        detectionMaterial = new Material(Shader.Find("Sprites/Default"));
        detectionMaterial.color = detectionColor;

        // 啟動偵測循環
        StartCoroutine(DetectionCycle());
    }

    void Update()
    {
        if (isDetecting)
        {
            UpdateDetectionArea(); // 繪製扇形範圍
            DetectObjectsInArea(); // 檢測碰撞
        }
    }

    private IEnumerator DetectionCycle()
    {
        while (true)
        {
            isDetecting = true;  // 開始偵測
            Debug.Log("開始偵測!!");
            yield return new WaitForSeconds(1f); // 啟動1秒

            isDetecting = false; // 停止偵測
            Debug.Log("停止偵測!!");
            yield return new WaitForSeconds(1f); // 關閉1秒
        }
    }

    private void UpdateDetectionArea()
    {
        // 計算扇形的頂點
        Vector3[] vertices = new Vector3[52]; // 50段 + 圓心 + 結尾
        int[] triangles = new int[150]; // 每段3個點，共50段

        vertices[0] = Vector3.zero; // 圓心
        float angleStep = detectionAngle / 50; // 每段的角度
        float startAngle = -detectionAngle / 2;

        for (int i = 0; i <= 50; i++)
        {
            float currentAngle = startAngle + angleStep * i;
            float radian = currentAngle * Mathf.Deg2Rad;
            vertices[i + 1] = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)) * detectionRadius;

            if (i < 50)
            {
                int triangleIndex = i * 3;
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = i + 1;
                triangles[triangleIndex + 2] = i + 2;
            }
        }

        // 更新 Mesh
        detectionMesh.Clear();
        detectionMesh.vertices = vertices;
        detectionMesh.triangles = triangles;
        detectionMesh.RecalculateNormals();

        // 繪製 Mesh
        Graphics.DrawMesh(detectionMesh, transform.localToWorldMatrix, detectionMaterial, 0);
    }

    private void DetectObjectsInArea()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);
        foreach (Collider2D collider in hitColliders)
        {
            // 計算目標與中心的方向向量
            Vector3 directionToTarget = collider.transform.position - transform.position;

            // 計算帶偏移的基準方向 (transform.up 經過旋轉)
            Vector3 rotatedUp = Quaternion.Euler(0, 0, detectionAngleOffset) * transform.up;

            // 計算目標角度
            float angleToTarget = Vector3.Angle(rotatedUp, directionToTarget);

            // 檢查是否在扇形範圍內
            if (angleToTarget <= detectionAngle / 2)
            {
                Debug.Log($"{collider.gameObject.name} 在範圍內！");
                // 在這裡處理 Game Over 邏輯
            }
        }
    }
}
