using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timerDuration = 62f; 
    private float timeRemaining;
    private bool next_state = false;
    public TextMeshProUGUI timerText;
    // public GameObject winScene;
    void Start()
    {
        timeRemaining = timerDuration; // Initialize the timer
        UpdateTimerDisplay();
        next_state = true;
        
    }

    void Update()
    {
        if (timeRemaining > 2.0f)
        {
            timeRemaining -= Time.deltaTime; // Reduce time by the elapsed frame time
            timeRemaining = Mathf.Max(timeRemaining, 0); // Clamp the timer at 0
            UpdateTimerDisplay();
        }
        else {
            // StartCoroutine(FadeIn());
            if(next_state==true){
            next_state = false;
            NextLevelController.instance.LoadNextScene();
            }

        }
    }
    // private IEnumerator FadeIn()
    // {
    //     float fadeDuration = 2.0f;

    //     winScene.SetActive(true);


    //     Image image = winScene.GetComponent<Image>();
    //     Color color = image.color;


    //     color.a = 0;
    //     image.color = color;

    //     float elapsedTime = 0;

    //     while (elapsedTime < fadeDuration) //�C�C�վ�z����
    //     {
    //         elapsedTime += Time.deltaTime;
    //         float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
    //         color.a = alpha;
    //         image.color = color;
    //         yield return null;
    //     }


    //     color.a = 1;
    //     image.color = color;
    // }

    void UpdateTimerDisplay()
    {
        // Format the time as whole seconds
        int seconds = Mathf.CeilToInt(timeRemaining -2.0f);
        timerText.text = seconds.ToString();
    }
}
