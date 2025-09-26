using System.Collections;
using UnityEngine;

public class floatingObjects : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f; // Time before the platform starts to fall
    [SerializeField] private float destroyDelay = 2f; // Time before the platform is destroyed after falling

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic; // Make the platform fall
        Destroy(gameObject, destroyDelay); // Destroy the platform after falling
    }
}
