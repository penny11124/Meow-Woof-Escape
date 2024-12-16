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
    public float detectionAngleOffset = 245f; // 偵測範圍的旋轉角度
    public Vector3 centerOffset = new Vector3(0.25f, 0.1f, 0f); // 圓心偏移量

    private Mesh detectionMesh;
    private Material detectionMaterial;
    private bool isDetecting = false;
    private GameObject detectionMeshObject; // 子物件的引用

    void Start()
    {
        // 自動追加 "Cat" 和 "Dog" 層到 detectionLayer
        AddLayersToDetection("Cat", "Dog");
        // 初始化 Mesh 和材質
        detectionMesh = new Mesh();
        detectionMaterial = new Material(Shader.Find("Sprites/Default"))
        {
            color = detectionColor
        };

        // 創建子物件來渲染 Mesh
        detectionMeshObject = new GameObject("DetectionMesh");
        detectionMeshObject.transform.SetParent(transform);
        detectionMeshObject.transform.localPosition = Vector3.zero;
        detectionMeshObject.transform.localRotation = Quaternion.identity;

        // 添加 MeshFilter 和 MeshRenderer 到子物件
        MeshFilter meshFilter = detectionMeshObject.AddComponent<MeshFilter>();
        meshFilter.mesh = detectionMesh;

        MeshRenderer meshRenderer = detectionMeshObject.AddComponent<MeshRenderer>();
        meshRenderer.material = detectionMaterial;
        meshRenderer.sortingLayerName = "item"; // 設定為目標 Sorting Layer
        meshRenderer.sortingOrder = 0; // 根據需要設定渲染順序

        // 偵測啟用時控制 Mesh 的可見性
        detectionMeshObject.SetActive(false);

        // 啟動偵測循環
        StartCoroutine(DetectionCycle());
    }

    void Update()
    {
        if (isDetecting)
        {
            // 啟用扇形並繪製
            detectionMeshObject.SetActive(true);
            UpdateDetectionArea();
            DetectObjectsInArea();
        }
        else
        {
            // 停止時隱藏扇形
            detectionMeshObject.SetActive(false);
        }
    }

    private IEnumerator DetectionCycle()
    {
        while (true)
        {
            isDetecting = true; // 開始偵測
            Debug.Log("開始偵測!!");
            yield return new WaitForSeconds(detectionDelay); // 啟動1秒

            isDetecting = false; // 停止偵測
            Debug.Log("停止偵測!!");
            yield return new WaitForSeconds(detectionDelay); // 關閉1秒
        }
    }

    private void AddLayersToDetection(params string[] layerNames)
    {
        foreach (string layerName in layerNames)
        {
            int layer = LayerMask.NameToLayer(layerName); // 根據層名獲取層編號
            if (layer >= 0) // 如果層存在
            {
                detectionLayer |= (1 << layer); // 使用位運算將該層加入 LayerMask
            }
            else
            {
                Debug.LogWarning($"圖層 '{layerName}' 不存在，請確保已創建此圖層");
            }
        }
    }

    private void UpdateDetectionArea()
    {
        // 計算扇形的頂點
        Vector3[] vertices = new Vector3[52]; // 50段 + 圓心 + 結尾
        int[] triangles = new int[150]; // 每段3個點，共50段

        vertices[0] = centerOffset; // 圓心設定為偏移位置
        float angleStep = detectionAngle / 50; // 每段的角度
        float startAngle = -detectionAngle / 2 + (detectionAngleOffset + 90); // 包含偏移量的起始角度

        for (int i = 0; i <= 50; i++)
        {
            float currentAngle = startAngle + angleStep * i;
            float radian = currentAngle * Mathf.Deg2Rad;
            Vector3 vertexPosition = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)) * detectionRadius;
            vertices[i + 1] = centerOffset + vertexPosition;

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

        // 自動渲染
        detectionMeshObject.GetComponent<MeshFilter>().mesh = detectionMesh;
    }

    private void DetectObjectsInArea()
    {
        // 偵測範圍基於圓心偏移位置
        Vector3 detectionCenter = transform.position + centerOffset;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionCenter, detectionRadius, detectionLayer);
        foreach (Collider2D collider in hitColliders)
        {
            // 計算目標與圓心的方向向量
            Vector3 directionToTarget = collider.transform.position - detectionCenter;

            // 計算帶偏移的基準方向 (考慮 GameObject 的 rotation)
            Quaternion rotation = transform.rotation; // 獲取旋轉
            Vector3 rotatedUp = rotation * (Quaternion.Euler(0, 0, detectionAngleOffset) * Vector3.up);

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
