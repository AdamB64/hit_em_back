using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class LoadSceneFixed : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "ExScene"; // Replace "GameScene" with your desired scene name

    // Public method to load the predetermined scene
    public void LoadScene()
    {
       //Debug.Log("Loading scene: "+ sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
