using UnityEngine;

public class ButtonWallTrigger : MonoBehaviour
{
    public Transform wall;          // 指定 Wall 的 Transform
    public Vector3 targetPosition;  // Wall 升起的目標位置
    public float riseSpeed = 2f;    // Wall 移動的速度

    private bool isDogOnButton = false; // 檢測狗是否站在按鈕上
    private Vector3 initialPosition;    // Wall 的初始位置

    private void Start()
    {
        // 記錄 Wall 的初始位置
        initialPosition = wall.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 當狗站上按鈕
        if (other.CompareTag("Dog"))
        {
            isDogOnButton = true;
            // Debug.Log("狗站在按鈕上！");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 當狗離開按鈕
        if (other.CompareTag("Dog"))
        {
            isDogOnButton = false;
            // Debug.Log("狗離開按鈕！");
        }
    }

    private void Update()
    {
        // 根據狗是否站在按鈕上控制 Wall 的位置
        if (isDogOnButton)
        {
            // 緩慢地將 Wall 移動到目標位置（升起）
            Vector3 newPosition = new Vector3(wall.position.x, targetPosition.y, wall.position.z);
            wall.position = Vector3.MoveTowards(wall.position, newPosition, riseSpeed * Time.deltaTime);
        }
        else
        {
            // 緩慢地將 Wall 移動回初始位置（下降）
            Vector3 newPosition = new Vector3(wall.position.x, initialPosition.y, wall.position.z);
            wall.position = Vector3.MoveTowards(wall.position, newPosition, riseSpeed * Time.deltaTime);
        }
    }
}
