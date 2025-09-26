using UnityEngine;

public class spike : MonoBehaviour
{
    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //anim.SetTrigger("die");
            Destroy(gameObject, 0.5f);
        }
    }

    
}
