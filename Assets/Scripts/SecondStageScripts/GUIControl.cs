using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIControl : MonoBehaviour
{
    public Canvas canvas; // 主 Canvas
    public Color edgeColor = new Color(1, 1, 1, 1);   // 两侧的白色
    public Color centerColor = new Color(0, 0, 0, 1); // 中央的黑色
    public float lineWidth = 10f; // 分割线的宽度（像素）
    public float lineHeight = 200f; // 分割线的高度（像素）

    void Start()
    {
        // 创建一个分割线 Image
        GameObject divider = new GameObject("Divider");
        divider.transform.SetParent(canvas.transform, false);

        // 添加 Image 组件
        Image image = divider.AddComponent<Image>();

        // 设置渐变纹理
        Texture2D gradientTexture = GenerateCenteredGradientTexture();
        Sprite gradientSprite = Sprite.Create(gradientTexture, new Rect(0, 0, gradientTexture.width, gradientTexture.height), new Vector2(0.5f, 0.5f));
        image.sprite = gradientSprite;

        // 设置 RectTransform
        RectTransform rectTransform = divider.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // 居中
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(lineWidth, lineHeight); // 设置宽度和高度
    }

    Texture2D GenerateCenteredGradientTexture()
    {
        int width = Mathf.RoundToInt(lineWidth);
        int height = Mathf.RoundToInt(lineHeight);
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            // 计算渐变比例 t（从 0 到 1）
            float t = Mathf.Abs((float)(x - width / 2) / (width / 2));
            Color color = Color.Lerp(centerColor, edgeColor, t); // 根据比例计算颜色

            for (int y = 0; y < height; y++)
            {
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }
}