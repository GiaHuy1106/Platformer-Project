using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject optionPanel; // panel chứa điều khiển âm thanh

    void Start()
    {
        if (optionPanel != null)
            optionPanel.SetActive(false); // ẩn panel lúc đầu
    }

    // Gọi hàm này khi bấm nút Play
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // load scene có index = 1
    }

    // Gọi hàm này khi bấm nút Option
    public void ToggleOptions()
    {
        if (optionPanel != null)
            optionPanel.SetActive(!optionPanel.activeSelf);
    }

    // Nếu bạn muốn thêm nút Quit
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game exited!");
    }

    public void Xbutton()
    {
        if (optionPanel != null)
            optionPanel.SetActive(false);
    }
}
