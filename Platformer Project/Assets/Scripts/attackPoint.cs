using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("hitPoint"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Enemy bị đánh trúng và bị hủy!");
        }
    }
}
