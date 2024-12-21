using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    // 按鈕點擊事件
    public void OnButtonClicked(string sceneName)
    {
        Debug.Log("Button clicked!!");

        // 如果場景名稱為空，則加載下一場景
        if (string.IsNullOrEmpty(sceneName))
        {
            NextLevelController.instance.LoadNextScene();
        }
        else
        {
            Debug.Log($"嘗試加載場景: {sceneName}");
            SceneTransitionManager.instance.LoadSceneWithTransition(sceneName);
        }
    }
}
