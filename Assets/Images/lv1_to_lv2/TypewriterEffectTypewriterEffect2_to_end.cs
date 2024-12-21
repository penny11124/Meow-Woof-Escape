using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffectTypewriterEffect2_to_end : MonoBehaviour
{
    public Text textComponent; // 要顯示的文字組件
    public Image imageComponent; // 要控制的圖片
    public Button buttonComponent; // 要控制的按鈕
    public float typeSpeed = 0.05f; // 每個字顯示的間隔時間
    public float fadeDuration = 2f; // 淡出持續時間
    public float buttonFadeDuration = 1f; // 按鈕淡入持續時間
    public float waitBeforeSwitch = 1f; // 淡出後等待時間

    [TextArea]
    public string fullText; // 完整的文字內容

    private string currentText = ""; // 當前顯示的文字

    void Start()
    {
        buttonComponent.gameObject.SetActive(false); // 初始隱藏按鈕
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

        // 淡入按鈕
        yield return StartCoroutine(FadeInButton());
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

    IEnumerator FadeInButton()
    {
        Debug.Log("Starting Button Fade In...");
        buttonComponent.gameObject.SetActive(true); // 顯示按鈕
        CanvasGroup buttonCanvasGroup = buttonComponent.GetComponent<CanvasGroup>();

        if (buttonCanvasGroup == null)
        {
            // 如果按鈕上沒有 CanvasGroup，添加一個
            buttonCanvasGroup = buttonComponent.gameObject.AddComponent<CanvasGroup>();
        }

        buttonCanvasGroup.alpha = 0f; // 初始透明度為 0
        float elapsedTime = 0f;

        while (elapsedTime < buttonFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            buttonCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / buttonFadeDuration);
            yield return null; // 等待下一幀
        }

        buttonCanvasGroup.alpha = 1f; // 確保完全顯示
        Debug.Log("Button Fade In Complete.");
    }
}
