using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance; // 單例實例
    public Animator animator;                      // 過渡動畫的 Animator
    public float transitionDuration = 1f;          // 過渡動畫的時間

    private void Awake()
    {
        // 確保只有一個實例存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 確保場景切換時不銷毀此物件
        }
        else
        {
            Destroy(gameObject); // 若已存在一個實例，銷毀多餘的物件
        }
    }

    // 公開方法來啟動場景過渡
    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // 1. 觸發淡出動畫
        animator.SetTrigger("StartFadeIn");

        // 2. 等待淡出動畫播放完成
        yield return new WaitForSeconds(transitionDuration);

        // 3. 開始非同步加載新場景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 禁止自動激活場景

        // 4. 等待場景加載完成至90%
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                break;
            }
            yield return null;
        }

        // 5. 激活場景
        asyncLoad.allowSceneActivation = true;

        // 6. 等待新場景激活完成
        yield return null;

        // 7. 觸發淡入動畫，並等待動畫播放完成
        animator.SetTrigger("StartFadeOut");
        yield return new WaitForSeconds(transitionDuration);
    }
}
