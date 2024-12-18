using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject; // 跟隨物件

    void Update()
    {
        if (targetObject != null)
        {
            transform.position = new Vector3(targetObject.position.x, targetObject.position.y+4, targetObject.position.z-10);
        }
    }
}
