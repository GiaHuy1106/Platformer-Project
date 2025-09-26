using UnityEngine;

public class cameraMove : MonoBehaviour
{
    [SerializeField] private float speed = 2f;        // tốc độ di chuyển camera
    [SerializeField] private Vector2 direction = Vector2.right; // hướng (phải mặc định)

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (cam == null) cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Di chuyển camera liên tục
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
