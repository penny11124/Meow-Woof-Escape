using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstStartGame : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float fadeDuration = 1.0f;

        Image image = gameObject.GetComponent<Image>();
        Color color = image.color;


        color.a = 255;
        image.color = color;

        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            color.a = alpha;
            image.color = color;
            yield return null;
        }


        color.a = 0;
        image.color = color;
        gameObject.SetActive(false);
    }
}
