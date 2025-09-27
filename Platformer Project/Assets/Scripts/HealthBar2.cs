using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar2 : MonoBehaviour
{
    public Slider healthBarSlider;
    public TextMeshProUGUI healthBarValueText;

    // Gọi khi set max máu
    public void SetMaxHealth(int maxHealth)
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
        healthBarValueText.text = maxHealth.ToString() + "/" + maxHealth.ToString();
    }

    // Gọi khi update máu hiện tại
    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
        healthBarValueText.text = health.ToString() + "/" + healthBarSlider.maxValue.ToString();
    }
}
