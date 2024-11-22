using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CheckAIObjects : MonoBehaviour
{
    public RawImage LevelCompleteIMG;
    void Update()
    {
        // Find all objects with the tag "AI"
        GameObject[] aiObjects = GameObject.FindGameObjectsWithTag("AI");

        // Check if all objects with the tag "AI" have been destroyed
        if (aiObjects.Length == 0)
        {
            LevelCompleteIMG.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
            // Load the scene "Level2"
            StartCoroutine(WaitAndExecute());
        }
    }
     IEnumerator WaitAndExecute()
    {
        // Wait for 30 seconds
        yield return new WaitForSeconds(10);

        // Code to execute after 30 seconds
        SceneManager.LoadScene("Level2");
    }
}
