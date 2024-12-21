using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlockGen : MonoBehaviour
{
    public GameObject[] blockPrefab; //傷害道具
    public GameObject goldenHeart; //補血道具
    public TextMeshProUGUI timer;
    public GameObject baby;
    private int health;
    private float currentTime;
    private int gameMode = 0; //遊戲難度
    private float spawnInterval;
    private float[] spawnPoints = new float[] {  2.0f, 4.0f, -2.0f, -4.0f};
    private float lastSpawnPoint = -99f;
    // Start is called before the first frame update
    void Start()
    {
        //UpdateSpawnInterval();
        //InvokeRepeating(nameof(SpawnBlock), 0, spawnInterval);
    }
    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(1.0f); 
    }
    // Update is called once per frame
    void Update()
    {
        health = baby.GetComponent<HealthControl>().Gethealth();
        if (float.TryParse(timer.text, out float parsedTime))
        {
            currentTime = parsedTime; // Update currentTime if parsing succeeds
        }
        else
        {
            Debug.LogError("Failed to parse timer text as a number.");
        }
        if (currentTime <= 59) {
            ChangeGameMode(currentTime);
        }
        
    }
    void ChangeGameMode(float time)
    {
        int newGameMode = 0;

        if (time <= 58 && time > 50)
        {
            newGameMode = 1;
        }
        else if (time <= 50 && time > 30)
        {
            newGameMode = 2;
            spawnPoints = new float[] { 0.0f, 2.0f, 4.0f, -2.0f, -4.0f };
        }
        else if (time <= 30 && time > 10)
        {
            newGameMode = 3;
            spawnPoints = new float[] { 0.0f, 1.8f, 3.6f, 5.4f, -1.8f, -3.6f, -5.4f };
        }
        else if (time <= 10 && time > 0)
        {
            newGameMode = 4;
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
                spawnInterval = 2.5f; // Slower spawning
                break;
            case 2:
                spawnInterval = 1.7f; // Medium spawning
                break;
            case 3:
                spawnInterval = 1.3f; // Faster spawning
                break;
            case 4:
                spawnInterval = 0.9f; // Faster spawning
                break;
            default:
                spawnInterval = 99.0f; // Barely spawning
                break;
        }

        // Start spawning blocks with the new interval
        InvokeRepeating(nameof(SpawnBlock), 0, spawnInterval);
    }

    void SpawnBlock()
    {
        int randomIndex = Random.Range(0,spawnPoints.Length); //隨機決定生成位置x
        float spawnX = spawnPoints[randomIndex];
        
        while (spawnX == lastSpawnPoint) {  //兩次隨機位置必不同
            randomIndex = Random.Range(0, spawnPoints.Length);
            spawnX = spawnPoints[randomIndex];
        }
        lastSpawnPoint = spawnX;

        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0); 

        int randomBlockIndex = Random.Range(0, blockPrefab.Length); //隨機決定生成物件
        GameObject block = blockPrefab[randomBlockIndex];

        if (health < 3)
        {
            int heartprob = Random.Range(0, 100);
            if (heartprob < 4)
            {
                Instantiate(goldenHeart, spawnPosition, Quaternion.identity);
            }
            else {
                Instantiate(block, spawnPosition, Quaternion.identity);
            }
        }
        else {
            Instantiate(block, spawnPosition, Quaternion.identity);
        }
        
    }

}
