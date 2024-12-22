using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // 用於場景加載

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu; // 拖入 PauseMenu 面板

    public bool isPaused = false; // 用來記錄暫停狀態
    private CatControls catControls; // 引用 CatControls
    private static PauseMenuController instance;
    private AudioSource[] audioSources;

    private void Awake()
    {
        catControls = new CatControls();

        // 確保只有一個實例存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 保留物件在場景之間切換
        }
        else
        {
            Destroy(gameObject); // 如果已有實例，銷毀重複物件
        }
        audioSources = GetComponents<AudioSource>();
    }

    private void Start()
    {
        catControls.Enable();
        pauseMenu.SetActive(false); // 隱藏暫停菜單
    }

    private void OnEnable()
    {
        // 訂閱 Pause 動作
        catControls.Cat.Pause.performed += OnPause;
        catControls.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 取消訂閱 Pause 動作
        catControls.Cat.Pause.performed -= OnPause;
        catControls.Disable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
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

    public void ResetLevel()
    {
        Debug.Log("重置場景！");
        Time.timeScale = 1f; // 確保時間恢復正常速度
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 加載當前場景
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 判斷是否需要禁用背景音樂
        if (scene.name == "ThirdScene") // 替換為特定場景名稱
        {
            if (audioSources[0] != null)
            {
                audioSources[0].Stop(); // 停止背景音樂
            }
            if (audioSources[1] != null)
            {
                audioSources[1].Play();
            }
        }
        
    }
}
