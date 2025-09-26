using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPersistence : MonoBehaviour
{
    private static PlayerPersistence instance;

    private void Awake()
    {
        // Nếu chưa có instance thì giữ lại Player
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Lắng nghe sự kiện khi scene load xong
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Tìm SpawnPoint trong scene mới
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy SpawnPoint trong scene: " + scene.name);
        }
    }

    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi Player bị hủy
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
