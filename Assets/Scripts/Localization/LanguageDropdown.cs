using UnityEngine;
using TMPro; // For TextMeshPro Dropdown
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown; // the dropdown in the Inspector

    private void Start()
    {
        // Set the dropdown options programmatically
        if (dropdown != null)
        {
            Debug.Log("Dropdown: " + dropdown);
            dropdown.ClearOptions();

            // Populate dropdown with language options
            dropdown.AddOptions(new System.Collections.Generic.List<string> { "English", "French", "German", "Spanish", "Italian" });

            // Set the dropdown to the current language
            string currentLanguage = LocalizationManager.Instance.GetCurrentLanguage();
            dropdown.value = GetDropdownIndexForLanguage(currentLanguage);

            // Add a listener for when the value changes
            dropdown.onValueChanged.AddListener(OnLanguageChanged);
        }
        Debug.Log("Dropdown: " + dropdown);
    }

    private void OnLanguageChanged(int selectedIndex)
    {
        // Map dropdown index to language code
        string selectedLanguage = GetLanguageCodeForDropdownIndex(selectedIndex);

        // Set the language in the LocalizationManager
        LocalizationManager.Instance.SetLanguage(selectedLanguage);

        // Refresh all localized text elements
        foreach (var localizedText in FindObjectsOfType<LocalizedText>())
        {
            localizedText.UpdateText();
        }
        Debug.Log("Selected language: " + selectedLanguage);
    }

    private int GetDropdownIndexForLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case "En": return 0;
            case "Fr": return 1;
            case "De": return 2;
            case "Es": return 3;
            case "It": return 4;
            default: return 0; // Default to English if language code is unrecognized
        }
    }

    private string GetLanguageCodeForDropdownIndex(int index)
    {
        switch (index)
        {
            case 0: return "En";
            case 1: return "Fr";
            case 2: return "De";
            case 3: return "Es";
            case 4: return "It";
            default: return "En"; // Default to English if index is unrecognized
        }
    }
}
