using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController  : MonoBehaviour
{
    public static NextLevelController  instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 場景切換時保持對象不被銷毀
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextScene()
    {
        // 自動加載下一關

        // 獲取當前場景索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"當前場景索引: {currentSceneIndex}");

        // 計算下一場景索引
        int nextSceneIndex = currentSceneIndex + 1;
        Debug.Log($"下依場景索引: {nextSceneIndex}");

        // 檢查是否存在下一場景
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // 使用 SceneUtility 獲取場景路徑
            string nextScenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            Debug.Log($"下一場景路徑: {nextScenePath}");

            // 從路徑中提取場景名稱
            string nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextScenePath);
            Debug.Log($"下一場景名稱: {nextSceneName}");

            // 調用場景切換管理器來加載場景
            SceneTransitionManager.instance.LoadSceneWithTransition(nextSceneName);
        }
        else
        {
            Debug.Log("已經是最後一關！無法加載下一場景。");
        }
    }
    
}
