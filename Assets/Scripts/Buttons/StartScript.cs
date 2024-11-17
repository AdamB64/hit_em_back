using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class LoadSceneStart : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "StartScene"; // Replace "GameScene" with your desired scene name

    // Public method to load the predetermined scene
    public void LoadStart()
    {
        //Debug.Log("Loading scene: "+ sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
