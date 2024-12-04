using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timerDuration = 60f; 
    private float timeRemaining;
    public TextMeshProUGUI timerText; 

    void Start()
    {
        timeRemaining = timerDuration; // Initialize the timer
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // Reduce time by the elapsed frame time
            timeRemaining = Mathf.Max(timeRemaining, 0); // Clamp the timer at 0
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        // Format the time as whole seconds
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = seconds.ToString();
    }
}
