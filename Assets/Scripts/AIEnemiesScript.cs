using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;  // Add this line to use UI components like RawImage

public class KnightAIWithTag : MonoBehaviour
{
    public float chaseRange = 10f; // Range within which the knight will start chasing
    public float attackRange = 2f; // Range within which the knight will attack
    public float detectionInterval = 1f; // How often to detect player for performance reasons
    private bool hasDealtDamage = false; // Tracks whether damage has been dealt during this attack

    private int count = 1; // Or use a bool if it's binary (true/false)
    public int health = 45;
    public RectTransform HealthIMG;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;
    private bool isPlayerDetected = false;
    private HealthBar playerStats;
    public GameObject playerObj;
    private float originalWidth; // Stores the original width of the health bar

    HealthBar Playerhealth;
    void Start()
    {

        originalWidth = HealthIMG.rect.width;

        //Debug.Log(Playerhealth);

        // Check if the MeshRenderer component is enabled
        Renderer enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer == null)
        {
            Debug.LogError("No Renderer component on character. Make sure the AI GameObject has a MeshRenderer or SkinnedMeshRenderer component.");
        }
        else
        {
            enemyRenderer.enabled = true; // Ensure the renderer is enabled
        }

        // Get the required components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
        {
            Debug.LogError("Missing NavMeshAgent component on " + gameObject.name);
        }

        if (animator == null)
        {
            Debug.LogError("Missing Animator component on " + gameObject.name);
        }

        // Start detecting the player
        InvokeRepeating(nameof(DetectPlayer), 0f, detectionInterval);
    }

    void Update()
    {
        if (agent == null || animator == null) return; // Exit if essential components are missing

        if (isPlayerDetected && player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= attackRange && IsPlayerInFront())
            {
                //Debug.Log(distanceToPlayer);Debug.Log(IsPlayerInFront());
                Attack(); // Keep attacking if the player is in range and in front
            }
            else if (distanceToPlayer <= chaseRange)
            {
                Chase(); // Chase the player if in range but not directly in front
            }
            else
            {
                Idle();
            }
        }
        else
        {
            Idle();
        }
    }

    void DetectPlayer()
    {
        // Find the player GameObject by tag
        playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            isPlayerDetected = true;
            Chase();
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    void Chase()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);

        if (agent != null && player != null)
        {
            agent.isStopped = false; // Ensure the NavMeshAgent is moving
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        player = playerObj.transform; // Assign the Transform
        playerStats = playerObj.GetComponent<HealthBar>(); // Get the PlayerStats script
        if (!IsPlayerInFront())
        {
            Chase(); // If the player isn't in front, switch to chasing
            return;
        }
        if (animator.GetBool("isAttacking")==false){
            count=1;
        }
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);

        while (count==1){

            if (agent != null && animator.GetBool("isAttacking")==true){
                agent.isStopped = true; // Stop moving while attacking
                playerStats.TakeDamage(5f); // Deal damage to the player
                count=0;
        }
        }

        if (player != null)
        {
            //Debug.Log(player);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z)); // Face the player
        }
    }

    void Idle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);

        if (agent != null)
        {
            agent.isStopped = true; // Stop moving
        }
    }

   bool IsPlayerInFront()
{
    // Calculate the direction vector from the AI to the player and normalize it
    Vector3 directionToPlayer = (player.position - transform.position).normalized;

    // Calculate the dot product between the AI's forward direction and the direction to the player
    float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

    // Calculate the distance between the AI and the player
    float distanceToPlayer = Vector3.Distance(player.position, transform.position);

    // Check two conditions:
    // 1. The dot product must be greater than 0.56f, meaning the player is roughly in front of the AI
    //    (0.8f * 0.7 = 0.56f, which is 30% larger than the original threshold).
    // 2. The distance to the player must be less than or equal to half the attack range, ensuring 
    //    the player is within a closer proximity.
    return dotProduct > 0.56f && distanceToPlayer <= attackRange / 2f;
}

    public void healthBar(float amount)
    {
        //Debug.Log(Playerhealth);
        //Playerhealth = playerObj.GetComponent<HealthBar>();
        //Debug.Log(Playerhealth);

        // Decrease health by the given amount
        originalWidth = HealthIMG.rect.width;
        health -= (int)amount;
        float healthPercentage = (float)health / 45f;
        Debug.Log(HealthIMG.sizeDelta);
        HealthIMG.sizeDelta = new Vector2(originalWidth * healthPercentage, HealthIMG.sizeDelta.y);
        Debug.Log(HealthIMG.sizeDelta);
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }


}
