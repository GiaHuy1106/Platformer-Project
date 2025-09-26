using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // <-- Add this line

public class playerMove : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private Image[] heartIcons;
    private bool isMoving = false;
    private Animator Anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerFight();
    }

    private void PlayerMove()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
        Anim.SetBool("Run", moveInput != 0);
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.y, jumpForce);
            isGrounded = false; // Prevent double jumping
        }
        Anim.SetBool("Jump", !isGrounded);
    }

    private void PlayerFight()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Anim.SetTrigger("Attack");
            Debug.Log("Player Attack!");
            StartCoroutine(ResetAttackAfterTime(0.5f));
        }
    }
    private IEnumerator ResetAttackAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Anim.ResetTrigger("Attack");
        Debug.Log("Attack animation auto reset sau " + time + "s");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Floating"))
        {
            isGrounded = true; // Player is grounded when colliding with ground or platform
            Debug.Log("Player is grounded");
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.y, jumpForce * 0.3f); // Bounce the player up slightly
            Destroy(gameObject); // Destroy player           
        }
        if (collision.gameObject.CompareTag("HitBox"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.3f); // Bounce the player up slightly
            playerHealth.instance.TakeDamage(1); // Reduce health by 1    
            //GameManager gameManager = FindAnyObjectByType<GameManager>();
            //gameManager.GameOver(); // Trigger game over in GameManager
            Debug.Log("Player hit enemy and game over");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        

        if (collision.gameObject.CompareTag("hitPoint"))
        {
            GameObject enemy = collision.transform.root.gameObject;
            Destroy(enemy); // Xóa luôn enemy
            Debug.Log("Enemy destroyed!");
        }

        if (collision.gameObject.CompareTag("Hearts"))
        {
            Debug.Log("+1");
            Destroy(collision.gameObject); // Destroy collectible
            playerHealth.instance.Heal(1); // Heal player by 1
            //GameManager gameManager = FindAnyObjectByType<GameManager>();
            //gameManager.AddScore(1); // Add score in GameManager
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            //Anim.SetTrigger("next level");
            SceneManager.LoadScene(1);
            Debug.Log("Level Complete! Load next level.");
        }
    }
}
