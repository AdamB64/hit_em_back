using UnityEngine;
using System.Collections;

public class RandomPrefabSpawner : MonoBehaviour
{
    [Header("Prefabs to Spawn")]
    public GameObject[] prefabs; // Array of prefabs to choose from.

    [Header("Respawn Settings")]
    public float minRespawnTime = 15f; // Minimum respawn time.
    public float maxRespawnTime = 45f; // Maximum respawn time.

    private GameObject currentInstance; // Current spawned prefab instance.
    private Collider objectCollider;    // Reference to the object's collider.

    private HealthBar Player;
    public GameObject playerObj;

    private void Start()
    {
        // Get the collider component.
        objectCollider = GetComponent<Collider>();

        SpawnRandomPrefab();
    }

    private void SpawnRandomPrefab()
    {
        // Destroy any existing instance.
        if (currentInstance != null)
        {
            Destroy(currentInstance);
        }

        // Randomly select a prefab and instantiate it.
        int randomIndex = Random.Range(0, prefabs.Length);
        currentInstance = Instantiate(prefabs[randomIndex], transform.position, transform.rotation, transform);

        // Enable the collider to allow interactions.
        objectCollider.enabled = true;
    }

    private IEnumerator RespawnAfterDelay()
    {
        // Wait for a random time between minRespawnTime and maxRespawnTime.
        float respawnTime = Random.Range(minRespawnTime, maxRespawnTime);
        yield return new WaitForSeconds(respawnTime);

        SpawnRandomPrefab();
    }

   void OnTriggerEnter(Collider other)
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        //Player = playerObj.transform; // Assign the Transform
        Player = playerObj.GetComponent<HealthBar>(); // Get the PlayerStats script
        if(Player.isSwordActive==true){
            Debug.Log("Sword is active");
        }
        else{
        // Check if the object colliding is the player (tag it appropriately).
        if (other.CompareTag("Player"))
        {
            // Disable the collider to prevent further interactions.
            GetComponent<Collider>().enabled = false;

            // Destroy the current instance to make it "disappear."
            if (currentInstance != null)
            {
                Destroy(currentInstance);
            }

            Player.sword(currentInstance);

            // Start the respawn process.
            StartCoroutine(RespawnAfterDelay());
        }
    }
    }
}
