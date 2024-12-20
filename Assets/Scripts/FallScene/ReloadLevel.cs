using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadLevel : MonoBehaviour
{
    public void Reload()
    {
        Debug.Log("try again button pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
