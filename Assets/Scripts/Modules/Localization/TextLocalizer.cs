using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalizer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private string localizationKey;
#pragma warning restore 0649

	private TextMeshProUGUI textMeshProUGUI;

	private void Start()
	{
		textMeshProUGUI = GetComponent<TextMeshProUGUI>();
		UpdateText();
		Modules.Localization.LanguageChanged += UpdateText;
	}

	private void UpdateText()
	{
		textMeshProUGUI.text = Modules.Localization.GetLocalizedText(localizationKey);
	}
}
