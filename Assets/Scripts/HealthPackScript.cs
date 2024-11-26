using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 15f; // Amount of health the pack heals
    public MeshRenderer meshRenderer; // Renderer for the health pack
    public Collider healthPackCollider; // Collider for the health pack

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
                // Disable the visual and collision components
                meshRenderer.enabled = false;
                healthPackCollider.enabled = false;

                StartCoroutine(RespawnHealth(30f)); // Respawn the health pack after a delay
            }
        }
    }
    private IEnumerator RespawnHealth(float delay)
    {
        //Debug.Log("ran");
        yield return new WaitForSeconds(delay);
        meshRenderer.enabled = true;
        healthPackCollider.enabled = true;
    }
}
