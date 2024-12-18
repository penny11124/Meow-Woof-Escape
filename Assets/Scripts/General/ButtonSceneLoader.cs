using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneLoader : MonoBehaviour
{
    //public  // 在 Inspector 中設置要加載的場景名稱
    public void OnButtonClicked(string sceneName)
    {
        // 調用 SceneTransitionManager 來加載場景
        Debug.Log("Button clicked!!");
        SceneTransitionManager.instance.LoadSceneWithTransition(sceneName);
    }
}
