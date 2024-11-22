using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class LoadLevel1 : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "Level1"; // Replace "GameScene" with your desired scene name

    // Public method to load the predetermined scene
    public void Level1()
    {
        //Debug.Log("Loading scene: "+ sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
