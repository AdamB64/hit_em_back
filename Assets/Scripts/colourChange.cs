using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Reference to the Renderer component of the cube
    private Renderer cubeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Renderer component of the cube
        cubeRenderer = GetComponent<Renderer>();
        
        // Change the color of the cube to black when the game starts
        cubeRenderer.material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally, change the color to black when space key is pressed
        if (Input.GetKeyDown(KeyCode.Space)) // If space key is pressed
        {
            // Change color to black when space key is pressed
            cubeRenderer.material.color = Color.black;
        }
    }
}
