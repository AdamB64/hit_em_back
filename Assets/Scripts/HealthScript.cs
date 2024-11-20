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
    public RawImage Sword;
    public RawImage HitSword;

    private bool Hit = false;
    private float originalWidth; // Stores the original width of the health bar

    [SerializeField]
    private string sceneName = "StartScene";

    void Start()
    {
        //Debug.Log("Start");
        // Cache the original width of the health bar
        originalWidth = healthBarImage.rect.width;
    }

    //void Update()
    //{
        //Debug.Log("Update");
        // Example input to simulate taking damage (press Space to take damage)
        //if (Input.GetKeyDown(KeyCode.H))
        //{
            //Debug.Log("H key pressed");
            //TakeDamage(10f); // Decrease health by 10
        //}
    //}

    void UpdateHealthBar()
    {
        //Debug.Log("UpdateHealthBar");
        // Calculate the health percentage (value between 0 and 1)
        float healthPercentage = currentHealth / maxHealth;

        // Adjust the width of the health bar based on the health percentage
        healthBarImage.sizeDelta = new Vector2(originalWidth * healthPercentage, healthBarImage.sizeDelta.y);
    }

    public void TakeDamage(float amount)
    {
        //Debug.Log("Player took damage: " + amount + " health");
        // Decrease health by the given amount
        currentHealth -= amount;
        if (currentHealth <= 0){
            currentHealth = 0;
            dead();
        };

        Hit=true;
        if(Hit){
            Debug.Log("Hit");
            StartCoroutine(Attack());
            Hit=false;
        }
        Sword.rectTransform.anchoredPosition = new Vector2(0,-400); // Adjust as needed
        HitSword.rectTransform.anchoredPosition = new Vector3(435,-166, 0); // Adjust as needed

        // Update the health bar
        UpdateHealthBar();
    }

    private IEnumerator Attack()
{
    Debug.Log("Attack");

    // Wait until the left mouse button is clicked
    while (!Input.GetMouseButtonDown(0))  // Wait until mouse button is clicked
    {
        yield return null;  // Wait one frame and then check again
    }

    // Once the button is clicked, proceed with the rest of the attack logic
    Debug.Log("Mouse Button Clicked");

    // Find all colliders within the radius
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
    Debug.Log("Number of colliders detected: " + hitColliders.Length);

    // Loop through all the colliders found in the radius
    foreach (Collider hitCollider in hitColliders)
    {
        // Check if the object has the "AI" tag
        if (hitCollider.CompareTag("AI"))
        {
            Debug.Log("AI Attack");

            // Get the HealthBar component (or equivalent) on the AI object
            KnightAIWithTag aiHealth = hitCollider.GetComponent<KnightAIWithTag>();

            Sword.rectTransform.anchoredPosition = new Vector3(435,-166,0); // Adjust as needed
            HitSword.rectTransform.anchoredPosition = new Vector3(0,-400, 0); // Adjust as needed

            aiHealth.healthBar(15f); // Deal damage to the AI
            
        }
    }
}
       

    private void dead(){
        Debug.Log("Dead");
    //FirstPersonController player = GetComponent<FirstPersonController>();


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
    Cursor.lockState = CursorLockMode.None;

    }

     IEnumerator WaitAndExecute()
    {
        // Wait for 30 seconds
        yield return new WaitForSeconds(10);

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