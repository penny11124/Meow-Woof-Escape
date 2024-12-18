using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        // 找到場景中的 GameManager
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager 未找到，請確保場景中有 GameManager 物件。");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Out"))
        {
            if (gameObject.CompareTag("Cat"))
            {
                gameManager.SetCatInOut(true);
                Debug.Log("貓已經碰到Out區域!");
            }
            else if (gameObject.CompareTag("Dog"))
            {
                gameManager.SetDogInOut(true);
                Debug.Log("狗已經碰到Out區域!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Out"))
        {
            if (gameObject.CompareTag("Cat"))
            {
                gameManager.SetCatInOut(false);
                Debug.Log("貓離開Out區域!");
            }
            else if (gameObject.CompareTag("Dog"))
            {
                gameManager.SetDogInOut(false);
                Debug.Log("狗離開Out區域!");
            }
        }
    }
}
