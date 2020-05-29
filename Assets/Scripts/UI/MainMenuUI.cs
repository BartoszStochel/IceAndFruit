using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private GameObject mainElements;
	[SerializeField] private GameObject tutorialElements;
#pragma warning restore 0649

	private void Start()
	{
		ShowMainMenu();
	}

	// Used by button in scene.
	public void StartGameplay()
	{
		SceneManager.LoadScene(Modules.SceneNames.GameplaySceneName);
	}

	// Used by button in scene.
	public void ShowTutorial()
	{
		mainElements.SetActive(false);
		tutorialElements.SetActive(true);
	}

	// Used by button in scene.
	public void ShowMainMenu()
	{
		mainElements.SetActive(true);
		tutorialElements.SetActive(false);
	}

	// Used by button in scene.
	public void ChangeLanguage(string languageIdentifier)
	{
		Modules.Localization.TryToSetLanguage(languageIdentifier);
	}
}
