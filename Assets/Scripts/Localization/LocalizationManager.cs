using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    private Dictionary<string, Dictionary<string, string>> localizedTexts;
    private string currentLanguage = "En"; // Default language

    [System.Serializable]
    private class LocalizationEntry
    {
        public string key;
        public Dictionary<string, string> translations;
    }

    [System.Serializable]
    private class LocalizationData
    {
        public List<LocalizationEntry> entries;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalizationFile();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadLocalizationFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Localization.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            LocalizationData data = JsonUtility.FromJson<LocalizationData>(jsonData);

            localizedTexts = new Dictionary<string, Dictionary<string, string>>();
            foreach (var entry in data.entries)
            {
                localizedTexts[entry.key] = entry.translations;
            }
        }
        else
        {
            Debug.LogError("Localization file not found at: " + filePath);
        }
    }

    public string GetLocalizedText(string objectKey)
    {
        if (localizedTexts != null && localizedTexts.ContainsKey(objectKey))
        {
            if (localizedTexts[objectKey].ContainsKey(currentLanguage))
            {
                return localizedTexts[objectKey][currentLanguage];
            }
        }

        Debug.LogWarning($"Translation not found for key: {objectKey}, language: {currentLanguage}");
        return objectKey; // Fallback to the key if translation is missing
    }

    public void SetLanguage(string languageCode)
    {
        currentLanguage = languageCode;
    }

    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }
}
