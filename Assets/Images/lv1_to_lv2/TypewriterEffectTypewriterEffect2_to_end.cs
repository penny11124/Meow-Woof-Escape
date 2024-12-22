using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffectTypewriterEffect2_to_end : MonoBehaviour
{
    public Text textComponent; // 要顯示的文字組件
    public Image imageComponent; // 要控制的圖片
    public Button skipButton; // 快轉按鈕
    public Button buttonComponent; // 要控制的按鈕
    public float typeSpeed = 0.05f; // 每個字顯示的間隔時間
    public float fadeDuration = 2f; // 淡出持續時間
    public float buttonFadeDuration = 1f; // 按鈕淡入持續時間
    public float waitBeforeSwitch = 1f; // 淡出後等待時間

    [TextArea]
    public string fullText; // 完整的文字內容

    private string currentText = ""; // 當前顯示的文字
    private bool isSkipping = false; // 是否正在快轉

    void Start()
    {
        buttonComponent.gameObject.SetActive(false); // 初始隱藏按鈕
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipTyping);
        }
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        Debug.Log("Starting Typewriter Effect...");

        for (int i = 0; i <= fullText.Length; i++)
        {
            if (isSkipping)
            {
                currentText = fullText;
                textComponent.text = currentText;
                break;
            }

            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(typeSpeed);
        }

        Debug.Log("Typewriter Effect Complete");

        if (skipButton != null)
        {
            skipButton.interactable = false;
        }

        yield return StartCoroutine(FadeOutTextAndImage());

        yield return new WaitForSeconds(waitBeforeSwitch);

        yield return StartCoroutine(FadeInButton());
    }

    IEnumerator FadeOutTextAndImage()
    {
        Debug.Log("Starting Fade Out...");
        float elapsedTime = 0f;

        Color textColor = textComponent.color;
        Color imageColor = imageComponent.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            textComponent.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            imageComponent.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);

            yield return null;
        }

        textComponent.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
        imageComponent.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
        Debug.Log("Fade Out Complete.");
    }

    IEnumerator FadeInButton()
    {
        Debug.Log("Starting Button Fade In...");
        buttonComponent.gameObject.SetActive(true);
        CanvasGroup buttonCanvasGroup = buttonComponent.GetComponent<CanvasGroup>();

        if (buttonCanvasGroup == null)
        {
            buttonCanvasGroup = buttonComponent.gameObject.AddComponent<CanvasGroup>();
        }

        buttonCanvasGroup.alpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < buttonFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            buttonCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / buttonFadeDuration);
            yield return null;
        }

        buttonCanvasGroup.alpha = 1f;
        Debug.Log("Button Fade In Complete.");
    }

    public void SkipTyping()
    {
        Debug.Log("Skip button pressed.");
        isSkipping = true;
    }
}
