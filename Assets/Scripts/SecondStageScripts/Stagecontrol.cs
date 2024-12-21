using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagecontrol : MonoBehaviour
{   
    public bool getNipple = false;
    public bool getBear = false;
    public bool trueEnd = false;
    public bool endStage = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(endStage && trueEnd){
            //True end
            NextLevelController.instance.LoadNextScene();
            endStage=false;
        }
        else if(endStage){
            //Bad end
            SceneTransitionManager.instance.LoadSceneWithTransition("lv2_to_end");
            endStage=false;
        }
    }
}
