using UnityEngine;
using UnityEngine.UI;  // Required for accessing Button component

public class QuitGame : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void Quit()
    {
        // If we are in the editor, stop playing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If we are not in the editor, quit the application
            Application.Quit();
        #endif
    }
}
