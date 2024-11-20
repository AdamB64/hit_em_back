using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 15f; // Amount of health the pack heals

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the player
        if (other.CompareTag("Player"))
        {
            HealthBar playerHealth = other.GetComponent<HealthBar>();
            
            if (playerHealth != null)
            {
                // Heal the player
                //Debug.Log(playerHealth.currentHealth);
                if (playerHealth.currentHealth == playerHealth.maxHealth)
                {
                    return; // Exit the method early if the player is already at full health
                }else{
                playerHealth.Heal(healAmount);
                }
                //Debug.Log(playerHealth.currentHealth);
                //Debug.Log("Player healed by " + healAmount + " health");
                Destroy(gameObject); // Destroy the health pack after use
            }
        }
    }
}
