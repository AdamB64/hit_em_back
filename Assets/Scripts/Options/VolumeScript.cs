using UnityEngine;
using UnityEngine.UI;

public class GameVolumeManager : MonoBehaviour
{
    [Header("Volume Controls")]
    public Scrollbar volumeScrollbar; // Reference to the volume scrollbar

    void Start()
    {
        // Set the scrollbar to match the current game volume
        volumeScrollbar.value = AudioListener.volume;
    }

    public void OnVolumeChange()
    {
        // Update the global game volume
        AudioListener.volume = volumeScrollbar.value*100;
        Debug.Log("Volume: " + volumeScrollbar.value*100);
    }
}
