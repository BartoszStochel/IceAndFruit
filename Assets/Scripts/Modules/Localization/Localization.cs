using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Localization : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private TextAsset localizationSheet;
#pragma warning restore 0649

	public event Action LanguageChanged;

	public ReadOnlyCollection<string> Languages { get; private set; }

	private string currentLanguage;
	private Dictionary<string, Dictionary<string, string>> languageDictionaries;
	private Dictionary<string, string> currentLanguageDictionary;

	public void Initialize()
	{
		InitializeWithLocalizationSheet();

		if (!TryToSetLanguage(Application.systemLanguage.ToString()))
		{
			TryToSetLanguage(Languages[0]);
		}
	}

	public bool TryToSetLanguage(string newLanguage)
	{
		if (!languageDictionaries.TryGetValue(newLanguage, out currentLanguageDictionary))
		{
			Debug.LogError($"There is no dictionary for this language: {newLanguage}");
			return false;
		}

		currentLanguage = newLanguage;
		LanguageChanged?.Invoke();
		return true;
	}

	public string GetLocalizedText(string key, params string[] values)
	{
		string localizedText;

		if (currentLanguageDictionary.TryGetValue(key, out localizedText))
		{
			for (int i = 0; i < values.Length; i++)
			{
				localizedText = localizedText.Replace("{" + i + "}", values[i]);
			}

			return localizedText;
		}
		else
		{
			Debug.LogError($"There is no translation for given key: {key}. Current language: {currentLanguage}");
		}

		return key;
	}

	private void InitializeWithLocalizationSheet()
	{
		List<List<string>> lines = Modules.TSVReader.ReadText(localizationSheet);

		InitializeLanguages(lines);
		InitializeLanguageDictionaries(lines);
	}

	private void InitializeLanguages(List<List<string>> lines)
	{
		List<string> languagesLine = lines[0];
		Languages = new ReadOnlyCollection<string>(languagesLine.GetRange(1, languagesLine.Count - 1));
	}

	private void InitializeLanguageDictionaries(List<List<string>> lines)
	{
		languageDictionaries = new Dictionary<string, Dictionary<string, string>>();

		for (int i = 0; i < Languages.Count; i++)
		{
			Dictionary<string, string> languageDictionary = new Dictionary<string, string>();

			for (int j = 1; j < lines.Count; j++)
			{
				List<string> lineValues = lines[j];
				languageDictionary.Add(lineValues[0], lineValues[i + 1]);
			}

			languageDictionaries.Add(Languages[i], languageDictionary);
		}
	}
}
