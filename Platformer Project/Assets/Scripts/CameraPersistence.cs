using UnityEngine;

public class CameraPersistence : MonoBehaviour
{
    private static CameraPersistence instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // giữ camera khi đổi scene
        }
        else
        {
            Destroy(gameObject); // tránh tạo camera trùng lặp
        }
    }
}
