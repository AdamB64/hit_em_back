using UnityEngine;

public class MatchCanvasSize : MonoBehaviour
{
    private RectTransform rectTransform; // Reference to the RectTransform of this GameObject
    private Canvas parentCanvas;         // Reference to the parent Canvas

    private void Start()
    {
        // Get the RectTransform component of the GameObject this script is attached to
        rectTransform = GetComponent<RectTransform>();

        // Find the parent Canvas
        parentCanvas = GetComponentInParent<Canvas>();

        if (rectTransform == null || parentCanvas == null)
        {
            Debug.LogError("Missing RectTransform or Canvas component. Ensure this script is attached to a UI GameObject inside a Canvas.");
            return;
        }

        // Adjust the size of the RectTransform to match the Canvas
        MatchSize();
    }

    private void MatchSize()
    {
        // Set the anchors to stretch across the parent
        rectTransform.anchorMin = Vector2.zero; // Bottom-left corner
        rectTransform.anchorMax = Vector2.one;  // Top-right corner

        // Reset offsets to stretch fully
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}
