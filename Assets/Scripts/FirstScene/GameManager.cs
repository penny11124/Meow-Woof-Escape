using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool catInOut = false; // 記錄貓是否碰到Out
    private bool dogInOut = false; // 記錄狗是否碰到Out
    private bool gameOver = false; // 遊戲是否已結束

    public GameObject failScene;

    public void SetCatInOut(bool state)
    {
        if(gameOver)
        {
            return;
        }
        catInOut = state;
        CheckWinCondition();
    }

    public void SetDogInOut(bool state)
    {
        if(gameOver)
        {
            return;
        }
        dogInOut = state;
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        // 當貓和狗都觸碰 Out 區域時觸發勝利
        if (catInOut && dogInOut)
        {
            Debug.Log("貓和狗都到達Out區域! 勝利!");
            StartCoroutine(HandleVictory());
        }
    }

    public void HandleFailure()
    {
        if (!gameOver)
        {
            gameOver = true;
            Debug.Log("遊戲失敗!");
            StartCoroutine(HandleGameOver());
        }
    }

    private IEnumerator HandleVictory()
    {
        yield return new WaitForSeconds(1f); // 延遲1秒
        SceneManager.LoadScene("lv1_to_lv2");
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(1f); // 延遲1秒
        StartCoroutine(FadeInLose());
    }

    private IEnumerator FadeInLose()
    {
        float fadeDuration = 2.0f;
        
        failScene.SetActive(true);

        
        Image image = failScene.GetComponent<Image>();
        Color color = image.color;

        
        color.a = 0;
        image.color = color;

        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration); 
            color.a = alpha; 
            image.color = color; 
            yield return null; 
        }

        color.a = 1;
        image.color = color;
    }

}
