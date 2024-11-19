using UnityEngine;

public class HPPopImage : MonoBehaviour
{
    // Reference to the image or object to move
    public RectTransform targetObject;

    // The target y-coordinate in world space
    private float targetY = 256f;

    // Public method to move the target object
    public void MoveToTargetY()
    {
        if (targetObject != null)
        {
            // Get the current world position
            Vector3 worldPosition = targetObject.position;

            // Set the new world position with target y-coordinate
            targetObject.position = new Vector3(worldPosition.x+4, targetY, worldPosition.z);
        }
        else
        {
            Debug.LogWarning("HPPopImage: No target object assigned.");
        }
    }
}