using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    private void Start()
    {
        // Populate the dropdown options if not already done in the Inspector
        //resolutionDropdown.ClearOptions();
        //resolutionDropdown.AddOptions(new System.Collections.Generic.List<string> { "1080p", "720p", "480p" });

        // Set default resolution to 1080p
        resolutionDropdown.value = 0;
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);
        //SetResolution(1920, 1080); // Default resolution
    }

    private void OnResolutionChange(int index)
    {
        switch (index)
        {
            case 0: // 1080p
                SetResolution(1920, 1080);
                break;
            case 1: // 720p
                SetResolution(1280, 720);
                break;
            case 2: // 480p
                SetResolution(640, 480);
                break;
            case 3: //140p
                SetResolution(140, 100);
                break;
        }
    }

    private void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, false);
        Debug.Log($"Resolution set to {width}x{height}");
        Debug.Log($"Current resolution: {Screen.width} x {Screen.height}, Fullscreen: {Screen.fullScreen}");
    }
}
