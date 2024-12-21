using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool catInOut = false; // 記錄貓是否碰到Out
    private bool dogInOut = false; // 記錄狗是否碰到Out
    private bool gameOver = false; // 遊戲是否已結束


    public void SetCatInOut(bool state)
    {
        if (!gameOver)
        {
            catInOut = state;
            CheckWinCondition();
        }
    }

    public void SetDogInOut(bool state)
    {
        if (!gameOver)
        {
            catInOut = state;
            CheckWinCondition();
        }
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
            gameOver = true; // 防止多次觸發失敗邏輯
            Debug.Log("遊戲失敗!");
            StartCoroutine(HandleGameOver());
        }
    }

    private IEnumerator HandleVictory()
    {
        yield return new WaitForSeconds(1f); // 延遲1秒
        SceneManager.LoadScene("FirstScene"); // 重新加載場景
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(1f); // 延遲1秒
        SceneManager.LoadScene("FirstScene"); // 重新加載場景
    }
}
