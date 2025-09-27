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
    public GameObject attackBox;
    public float attackDuration = 1; // thời gian attack tồn tại
    private float attackTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Anim.SetBool("Attack", false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerFight();

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;

            // Hết thời gian -> reset
            if (attackTimer <= 0)
            {
                attackBox.SetActive(false);
                Anim.SetBool("Attack", false);
                Debug.Log("Attack animation auto reset sau " + attackDuration + "s");
            }
        }
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
        //SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxJump);
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.y, jumpForce);
            isGrounded = false; // Prevent double jumping
            Anim.SetBool("Jump", !isGrounded);
            //SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxJump);
        }
        
    }

    private void PlayerFight()
    {
        if (Input.GetKeyDown(KeyCode.E) && attackTimer <= 0)
        {
            Anim.SetBool("Attack", true);
            Debug.Log("Player Attack!");

            // Bật hitbox khi bắt đầu tấn công
            attackBox.SetActive(true);

            // Bắt đầu đếm thời gian
            attackTimer = attackDuration;
            //SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxAttack);
        }
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
            PlayerHealth.Instance.TakeDamage(1); // Reduce health by 1    
            //HealthManager.instance.TakeDamage(10); // Reduce health by 20
            Debug.Log("Player hit enemy");
            //SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxHit);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hitPoint"))
        {
            GameObject enemy = collision.transform.root.gameObject;
            Destroy(enemy); // Xóa luôn enemy
            Debug.Log("Enemy destroyed!");
            //SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxAttack);
        }

        

        if (collision.gameObject.CompareTag("Hearts"))
        {
            Debug.Log("+1");
            Destroy(collision.gameObject); // Destroy collectible
            //playerHealth.instance.Heal(1); // Heal player by 1
            //HealthManager.instance.Heal(10);
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            //Anim.SetTrigger("next level");
            SceneManager.LoadScene(2);
            Debug.Log("Level Complete! Load next level.");
        }
    }
}
