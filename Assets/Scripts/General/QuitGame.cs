using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // 在 Unity 編輯器或構建的遊戲中執行退出
    public void ExitGame()
    {
        Debug.Log("退出遊戲！");
        Application.Quit();

        // 如果在 Unity 編輯器中測試，停止播放模式
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
