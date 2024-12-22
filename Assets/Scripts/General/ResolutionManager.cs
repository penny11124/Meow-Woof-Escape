using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public int targetWidth = 1920; // 16:9 寬度
    public int targetHeight = 1080; // 16:9 高度
    public bool isFullScreen = true; // 是否全屏

    void Start()
    {
        // 設定解析度
        SetResolution();
    }

    void SetResolution()
    {
        // 計算當前螢幕比例
        float targetAspect = (float)targetWidth / targetHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        if (Mathf.Abs(targetAspect - currentAspect) > 0.01f)
        {
            Debug.Log($"螢幕比例與目標比例不同，鎖定為 16:9，當前比例: {currentAspect}");
        }

        // 強制設定目標解析度並啟用全屏模式
        Screen.SetResolution(targetWidth, targetHeight, isFullScreen);
        Debug.Log($"解析度已設定為 {targetWidth}x{targetHeight}，全屏: {isFullScreen}");
    }
}
