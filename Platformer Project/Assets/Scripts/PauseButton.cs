using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseButton : MonoBehaviour
{
    public static PauseButton instance;
    private bool isPaused = false;
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Đảm bảo panel ẩn khi bắt đầu
        }
    }

    public void TogglePause()
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
        Time.timeScale = 0f;  // Dừng toàn bộ chuyển động
        isPaused = true;       
        if (pausePanel != null)
        {
            pausePanel.SetActive(true); // Hiển thị panel tạm dừng
        }
        Debug.Log("Button Pressed");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;  // Tiếp tục như cũ
        isPaused = false;
        pausePanel.SetActive(false); // Ẩn panel tạm dừng
    }

    public void closePanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Ẩn panel tạm dừng
        }
    }    
}
