using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu; // 拖入 PauseMenu 面板

    private bool isPaused = false; // 用來記錄暫停狀態
    private CatControls controls; // 引用 CatControls

    private void Awake()
    {
        // 初始化 CatControls
        controls = new CatControls();
        Debug.Log("CatControls 初始化完成");
    }

    private void Start()
    {
        // 確保輸入系統啟用
        controls.Enable();
        Debug.Log("隱藏暫停菜單");
        pauseMenu.SetActive(false); // 隱藏暫停菜單
    }

    private void OnEnable()
    {
        // 訂閱 Pause 動作
        controls.Cat.Pause.performed += OnPause;
        controls.Enable();
        Debug.Log("CatControls 已啟用");
    }

    private void OnDisable()
    {
        // 取消訂閱 Pause 動作
        controls.Cat.Pause.performed -= OnPause;
    }

    private void Update()
    {
        // 後備方案：直接檢測 Escape 鍵
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("直接按鍵檢測：收到 Escape 按鍵！");
            OnPause(new InputAction.CallbackContext());
        }
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause 按鍵觸發");
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Debug.Log("暫停遊戲！");
        pauseMenu.SetActive(true); // 顯示暫停菜單
        Time.timeScale = 0f;       // 停止遊戲時間
        isPaused = true;           // 設置為暫停狀態
    }

    public void ResumeGame()
    {
        Debug.Log("繼續遊戲！");
        pauseMenu.SetActive(false); // 隱藏暫停菜單
        Time.timeScale = 1f;        // 恢復遊戲時間
        isPaused = false;           // 設置為非暫停狀態
    }
}
