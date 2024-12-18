using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool catInOut = false; // 記錄貓是否碰到Out
    private bool dogInOut = false; // 記錄狗是否碰到Out

    public void SetCatInOut(bool state)
    {
        catInOut = state;
        CheckWinCondition();
    }

    public void SetDogInOut(bool state)
    {
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

    private IEnumerator HandleVictory()
    {
        yield return new WaitForSeconds(1f); // 延遲1秒
        SceneManager.LoadScene("FirstScene"); // 重新加載場景
    }
}
