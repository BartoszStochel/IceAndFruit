using UnityEngine;

[CreateAssetMenu(fileName = "SceneNames", menuName = "Configuration/Scene names container")]
public class SceneNames : ScriptableObject
{
#pragma warning disable 0649
	[SerializeField] private string mainMenuSceneName;
	[SerializeField] private string gameplaySceneName;
#pragma warning restore 0649

	public string MainMenuSceneName => mainMenuSceneName;
	public string GameplaySceneName => gameplaySceneName;
}
