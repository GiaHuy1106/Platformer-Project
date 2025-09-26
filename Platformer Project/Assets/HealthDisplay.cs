using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Sprite fullHeart;
    public GameObject[] hearts;

    public playerHealth PlayerHealth;

    private void Update()
    {
        int health = PlayerHealth.currentHealth;
        int maxHealth = PlayerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            Image heartImage = hearts[i].GetComponent<Image>();

            if (i < maxHealth)
            {
                // Hiện tim trong phạm vi maxHealth
                hearts[i].SetActive(true);

                if (i < health)
                {
                    // Còn máu -> hiện tim đầy
                    heartImage.sprite = fullHeart;
                }
                else
                {
                    // Mất máu -> ẩn tim đó
                    hearts[i].SetActive(false);
                }
            }
            else
            {
                // Ẩn tim dư so với maxHealth
                hearts[i].SetActive(false);
            }
        }
    }
}
