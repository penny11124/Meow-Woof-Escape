using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Continue : MonoBehaviour
{
    public void NextLevel(string nextScene)
    {
        Debug.Log("Change the scene to: " + nextScene);
        NextLevelController.instance.LoadNextScene();
    }
}
