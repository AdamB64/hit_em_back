using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string objectKey; // Key to look up in JSON

    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
        Debug.Log("Text component: " + textComponent);
        UpdateText();
    }

    public void UpdateText()
    {
        if (LocalizationManager.Instance != null && textComponent != null)
        {
            textComponent.text = LocalizationManager.Instance.GetLocalizedText(objectKey);
        }
    }
}
