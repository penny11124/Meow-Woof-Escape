using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu; // 拖入 PauseMenu 面板

    private bool isPaused = false; // 用來記錄暫停狀態
    private CatControls catControls; // 引用 CatControls
    // private DogControls dogControls; // 引用 DogControls

    private void Awake()
    {
        // Debug.Log("PauseMenuController: Awake 被調用！");
        // 初始化 CatControls
        catControls = new CatControls();
        // dogControls = new DogControls();
        Debug.Log("CatControls 初始化完成");
    }

    private void Start()
    {
        // Debug.Log("PauseMenuController: Start 被調用！");
        catControls.Enable();
        // dogControls.Enable();
        Debug.Log("Start內隱藏暫停菜單");
        pauseMenu.SetActive(false); // 隱藏暫停菜單
    }

    private void OnEnable()
    {
        // 訂閱 Pause 動作
        catControls.Cat.Pause.performed += OnPause;
        catControls.Enable();
        // dogControls.Dog.Pause.performed += OnPause;
        // dogControls.Enable();
        // Debug.Log("CatControls 已啟用");
    }

    private void OnDisable()
    {
        // 取消訂閱 Pause 動作
        catControls.Cat.Pause.performed -= OnPause;
        catControls.Disable();
        // dogControls.Dog.Pause.performed -= OnPause;
        // dogControls.Disable();
        // Debug.Log("CatControls 已禁用");
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        // Debug.Log("Pause 按鍵觸發");
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
