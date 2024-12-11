using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlockGen : MonoBehaviour
{
    public GameObject blockPrefab;
    public TextMeshProUGUI timer;
    private float currentTime;
    private int gameMode = 0; //¹CÀ¸Ãø«×
    private float spawnInterval;
    private float[] spawnPoints = new float[] { 0.0f, 2.0f, 4.0f, -2.0f, -4.0f };
    // Start is called before the first frame update
    void Start()
    {
        UpdateSpawnInterval();
        InvokeRepeating(nameof(SpawnBlock), 0, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (float.TryParse(timer.text, out float parsedTime))
        {
            currentTime = parsedTime; // Update currentTime if parsing succeeds
            //Debug.Log(gameMode);
        }
        else
        {
            Debug.LogError("Failed to parse timer text as a number.");
        }
        
        
    }
    void ChangeGameMode(float time)
    {
        int newGameMode = 0;

        if (time <= 60 && time > 50)
        {
            newGameMode = 1;
        }
        else if (time <= 50 && time > 30)
        {
            newGameMode = 2;
        }
        else if (time <= 30 && time > 0)
        {
            newGameMode = 3;
        }

        if (newGameMode != gameMode)
        {
            gameMode = newGameMode;
            Debug.Log("Game Mode changed to: " + gameMode);

            // Update the spawn interval when game mode changes
            UpdateSpawnInterval();
        }
    }
    void UpdateSpawnInterval()
    {
        CancelInvoke(nameof(SpawnBlock)); // Stop the current spawning task

        // Set spawn interval based on the current game mode
        switch (gameMode)
        {
            case 1:
                spawnInterval = 3.0f; // Slower spawning
                break;
            case 2:
                spawnInterval = 2.0f; // Medium spawning
                break;
            case 3:
                spawnInterval = 1.0f; // Faster spawning
                break;
            default:
                spawnInterval = 3.0f; // Default slow spawning
                break;
        }

        // Start spawning blocks with the new interval
        InvokeRepeating(nameof(SpawnBlock), 0, spawnInterval);
    }

    void SpawnBlock()
    {
        int randomIndex = Random.Range(0,spawnPoints.Length);
        float spawnX = spawnPoints[randomIndex]; // Random X position
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0);
        Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
    }

}
