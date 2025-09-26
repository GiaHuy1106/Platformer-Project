using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public static playerHealth instance;
    public int maxHealth = 5;
    public int currentHealth;
    public SpriteRenderer playerSr;
    public playerMove playerMovement;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            playerSr.enabled = false;
            playerMovement.enabled = false;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        
        Debug.Log("Player has died");
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
