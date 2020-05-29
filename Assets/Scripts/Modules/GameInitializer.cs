using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private Balance balance;
	[SerializeField] private Timers timers;
	[SerializeField] private Localization localization;
	[SerializeField] private SceneNames sceneNames;
#pragma warning restore 0649

	private void Start()
	{
		Modules.InitializeModules(balance, timers, localization, sceneNames);

		DontDestroyOnLoad(gameObject);
		Destroy(this);

		SceneManager.LoadScene(Modules.SceneNames.MainMenuSceneName);
	}
}
