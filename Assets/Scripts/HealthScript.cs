using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // Required for scene management


public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health
    public float currentHealth = 100f; // Current health

    public RectTransform healthBarImage; // Reference to the RectTransform of the health bar Image
    public RawImage DeadImage;

    private float originalWidth; // Stores the original width of the health bar

    [SerializeField]
    private string sceneName = "StartScene";

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
        if (currentHealth <= 0){
            currentHealth = 0;
            dead();
        };

        // Update the health bar
        UpdateHealthBar();
    }

    void dead(){
    // Check if DeadImage is assigned
    if (DeadImage == null)
    {
        Debug.LogError("DeadImage is not assigned in the Inspector!");
        return; // Exit the method early to avoid further errors
    }

    // Check if the DeadImage has a valid rectTransform
    if (DeadImage.rectTransform == null)
    {
        Debug.LogError("DeadImage.rectTransform is null!");
        return; // Exit the method early to avoid further errors
    }

    // Move the DeadImage immediately (example: set position to center)
    DeadImage.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
    DeadImage.gameObject.SetActive(true); // Ensure it is visible

    // Start the coroutine for delayed actions
    StartCoroutine(WaitAndExecute());

    }

     IEnumerator WaitAndExecute()
    {
        // Wait for 30 seconds
        yield return new WaitForSeconds(15);

        // Code to execute after 30 seconds
        Debug.Log("30 seconds have passed!");
        SceneManager.LoadScene(sceneName);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure health doesn't exceed max health
        Debug.Log("Player Healed. Current Health: " + currentHealth);
        UpdateHealthBar();
    }
}
