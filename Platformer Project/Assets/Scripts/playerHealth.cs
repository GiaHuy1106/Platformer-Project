using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance; // Singleton
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar2 healthBar; // Tham chiếu đến script HealthBar2

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);   // Cập nhật UI ngay khi start
        healthBar.SetHealth(currentHealth);
    }

    // Hàm nhận sát thương
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Không cho < 0
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Hàm hồi máu
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Không vượt quá max
        healthBar.SetHealth(currentHealth);
    }

    // Khi máu = 0
    private void Die()
    {
        Debug.Log("Player đã chết!");
        // Thêm logic chết ở đây (animation, respawn, load scene...)
    }
}
