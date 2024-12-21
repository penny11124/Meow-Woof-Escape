using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstTimer : MonoBehaviour
{
    public float timerDuration = 200f; 
    public float timeRemaining;
    private int falling;
    public TextMeshProUGUI timerText; 
    private GameManager gameManager; // 引用 GameManager

    void Start()
    {
        timeRemaining = timerDuration; // Initialize the timer
        UpdateTimerDisplay();
        falling = 1;

        // 獲取 GameManager 引用
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("找不到 GameManager!");
        }
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // Reduce time by the elapsed frame time
            timeRemaining = Mathf.Max(timeRemaining, 0); // Clamp the timer at 0
            UpdateTimerDisplay();

            // Debug.Log("timeRemaining: " + timeRemaining);
            // 當時間結束時觸發遊戲失敗
            // if (timeRemaining <= 0)
            // {
            //     gameManager?.HandleFailure();
            // }
        }
        else {
            if(falling==1){
                gameManager?.HandleFailure();
                falling = 0;
            }
            
            
        }
    }

    void UpdateTimerDisplay()
    {
        // Format the time as whole seconds
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = seconds.ToString();
    }

    public void ModifyTime(float amount)
    {
        timeRemaining = Mathf.Max(timeRemaining + amount, 0); // 扣除時間並確保不低於 0
        UpdateTimerDisplay();
    }

}
