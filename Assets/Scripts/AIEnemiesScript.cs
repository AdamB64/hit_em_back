using UnityEngine;
using UnityEngine.AI;

public class KnightAIWithTag : MonoBehaviour
{
    public float chaseRange = 10f; // Range within which the knight will start chasing
    public float attackRange = 2f; // Range within which the knight will attack
    public float detectionInterval = 1f; // How often to detect player for performance reasons

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;
    private bool isPlayerDetected = false;

    void Start()
    {
        // Check if the MeshRenderer component is enabled
        Renderer enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer == null)
        {
            Debug.LogError("No Renderer component found. Make sure the AI GameObject has a MeshRenderer or SkinnedMeshRenderer component.");
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

            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else if (distanceToPlayer <= chaseRange)
            {
                Chase();
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
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            isPlayerDetected = true;
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
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);

        if (agent != null)
        {
            agent.isStopped = true; // Stop moving while attacking
        }

        if (player != null)
        {
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
}
