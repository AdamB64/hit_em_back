using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health
    public float currentHealth = 100f; // Current health

    public RectTransform healthBarImage; // Reference to the RectTransform of the health bar Image

    private float originalWidth; // Stores the original width of the health bar

    void Start()
    {
        //Debug.Log("Start");
        // Cache the original width of the health bar
        originalWidth = healthBarImage.rect.width;
    }

    void Update()
    {
        //Debug.Log("Update");
        // Example input to simulate taking damage (press Space to take damage)
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Debug.Log("H key pressed");
            TakeDamage(10f); // Decrease health by 10
        }
    }

    void UpdateHealthBar()
    {
        //Debug.Log("UpdateHealthBar");
        // Calculate the health percentage (value between 0 and 1)
        float healthPercentage = currentHealth / maxHealth;

        // Adjust the width of the health bar based on the health percentage
        healthBarImage.sizeDelta = new Vector2(originalWidth * healthPercentage, healthBarImage.sizeDelta.y);
    }

    void TakeDamage(float amount)
    {
        // Decrease health by the given amount
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        // Update the health bar
        UpdateHealthBar();
    }

    void Heal(float amount)
    {
        // Increase health by the given amount
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        // Update the health bar
        UpdateHealthBar();
    }
}
