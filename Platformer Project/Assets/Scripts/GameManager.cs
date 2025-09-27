using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject instructionPanel; 
    

    void Start()
    {
        if (optionPanel != null)
            optionPanel.SetActive(false); // ẩn panel lúc đầu
        if (instructionPanel != null)
            instructionPanel.SetActive(false); // ẩn panel lúc đầu
    }

    // Gọi hàm này khi bấm nút Play
    public void Play()
    {
        instructionPanel.SetActive(true);
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

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }    
}
