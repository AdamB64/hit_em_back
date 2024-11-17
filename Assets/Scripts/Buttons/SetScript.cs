using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class LoadSceneSettings : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "SetScene"; // Replace "GameScene" with your desired scene name

    // Public method to load the predetermined scene
    public void LoadSettings()
    {
        //Debug.Log("Loading scene: "+ sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
