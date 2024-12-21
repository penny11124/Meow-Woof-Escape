using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public Text textComponent; // 要顯示的文字組件
    public Image imageComponent; // 要控制的圖片
    public float typeSpeed = 0.05f; // 每個字顯示的間隔時間
    public float fadeDuration = 2f; // 淡出持續時間
    public float waitBeforeSwitch = 1f; // 淡出後等待時間

    [TextArea]
    public string fullText; // 完整的文字內容

    private string currentText = ""; // 當前顯示的文字

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        Debug.Log("Starting Typewriter Effect...");

        // 打字機效果逐字顯示文字
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText; // 更新文字內容
            yield return new WaitForSeconds(typeSpeed); // 等待
        }

        Debug.Log("Typewriter Effect Complete");

        // 打字完成後淡出文字和圖片
        yield return StartCoroutine(FadeOutTextAndImage());

        // 等待指定時間
        yield return new WaitForSeconds(waitBeforeSwitch);

        // 切換到下一個場景
        NextLevelController.instance.LoadNextScene();
    }

    IEnumerator FadeOutTextAndImage()
    {
        Debug.Log("Starting Fade Out...");
        float elapsedTime = 0f;

        // 獲取文字和圖片的初始顏色
        Color textColor = textComponent.color;
        Color imageColor = imageComponent.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // 更新文字和圖片的透明度
            textComponent.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            imageComponent.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);

            yield return null; // 等待下一幀
        }

        // 最後確保完全隱藏
        textComponent.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
        imageComponent.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
        Debug.Log("Fade Out Complete.");
    }
}
