using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty { Easy, Normal, Hard }
    public static DifficultyManager Instance { get; private set; }

    public Difficulty currentDifficulty = Difficulty.Normal;

    private Dropdown difficultyDropdown; // Dropdown for difficulty selection

    private void Awake()
    {
        // Ensure this manager persists across scenes and only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load events
        FindDropdownInCurrentScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindDropdownInCurrentScene(); // Reassign dropdown after scene loads
    }

    private void FindDropdownInCurrentScene()
    {
        // Look for the dropdown tagged "Difficulty"
        GameObject dropdownObject = GameObject.FindGameObjectWithTag("difficulty");

        if (dropdownObject != null)
        {
            // Get the Dropdown component
            difficultyDropdown = dropdownObject.GetComponent<Dropdown>();

            if (difficultyDropdown != null)
            {
                // Sync dropdown value with the current difficulty
                difficultyDropdown.value = (int)currentDifficulty;

                // Ensure the dropdown listener is only added once
                difficultyDropdown.onValueChanged.RemoveListener(SetDifficultyFromDropdown);
                difficultyDropdown.onValueChanged.AddListener(SetDifficultyFromDropdown);
            }
            else
            {
                Debug.LogWarning("DifficultyManager: Dropdown component missing on object tagged 'Difficulty'.");
            }
        }
        else
        {
            Debug.Log("DifficultyManager: No dropdown tagged 'Difficulty' in the current scene.");
        }
    }

    public void SetDifficultyFromDropdown(int dropdownValue)
    {
        // Update the difficulty based on the dropdown value
        currentDifficulty = (Difficulty)dropdownValue;
        Debug.Log($"Difficulty set to: {currentDifficulty}");
    }

    public float GetEnemyHealth()
    {
        // Return health based on the current difficulty
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                return 30f;
            case Difficulty.Normal:
                return 45f;
            case Difficulty.Hard:
                return 50f;
            default:
                return 45f;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene load events to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
