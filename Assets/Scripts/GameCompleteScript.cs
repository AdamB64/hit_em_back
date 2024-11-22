using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CheckGameObjects : MonoBehaviour
{
    public RawImage GameOverIMG;
    void Update()
    {
        // Find all objects with the tag "AI"
        GameObject[] aiObjects = GameObject.FindGameObjectsWithTag("AI");

        // Check if all objects with the tag "AI" have been destroyed
        if (aiObjects.Length == 0)
        {
            GameOverIMG.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
            // Load the scene "Level2"
            StartCoroutine(WaitToExecute());
            Cursor.lockState = CursorLockMode.None;
        }
    }
     IEnumerator WaitToExecute()
    {
        // Wait for 30 seconds
        yield return new WaitForSeconds(10);

        // Code to execute after 30 seconds
        SceneManager.LoadScene("StartScene");
    }
}
