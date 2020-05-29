using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameplayUI : MonoBehaviour
{
	private const string SCORE_LOCALIZATION_KEY = "gameplayScore";
	private const string CURRENT_TURN_LOCALIZATION_KEY = "gameplayCurrentTurn";
	private const string GAME_OVER_LOCALIZATION_KEY = "youLoseSummaryText";

#pragma warning disable 0649
	[SerializeField] private GameplayManager gameplayManager;
	[SerializeField] private TileContentSprites tileContentSprites;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI currentTurnText;
	[SerializeField] private Transform nextContentImagesParent;
	[SerializeField] private GameObject nextContentImagePrefab;
	[SerializeField] private GameObject gameOverWindow;
	[SerializeField] private TextMeshProUGUI gameOverText;
	[SerializeField] private GameObject pauseMenu;
#pragma warning restore 0649

	private List<Image> nextContentImages = new List<Image>();

	private void Awake()
	{
		gameplayManager.Initialized += Initialize;
	}

	// Used by button in scene.
	public void OpenPauseMenu()
	{
		pauseMenu.SetActive(true);
	}

	// Used by button in scene.
	public void ClosePauseMenu()
	{
		pauseMenu.SetActive(false);
	}

	// Used by button in scene.
	public void GoBackToMainMenu()
	{
		gameplayManager.GoBackToMainMenu();
	}

	private void Initialize()
	{
		gameplayManager.NewTurnHasBegun += UpdateTurnInfo;
		gameplayManager.Board.BoardNewContentPlacer.NewContentToPlaceIsChosen += UpdateNextContentIndicator;
		gameplayManager.GameEnded += ShowGameOverWindow;

		UpdateTurnInfo(0, 0);

		gameOverWindow.SetActive(false);
		pauseMenu.SetActive(false);
	}

	private void UpdateTurnInfo(int newScoreValue, int newTurnNumber)
	{
		scoreText.text = Modules.Localization.GetLocalizedText(SCORE_LOCALIZATION_KEY) + ": " + newScoreValue;
		currentTurnText.text = Modules.Localization.GetLocalizedText(CURRENT_TURN_LOCALIZATION_KEY) + ": " + newTurnNumber;
	}

	private void UpdateNextContentIndicator(List<(TileContent contentType, int fruitID)> nextContent)
	{
		CreateNewNextContentImagesIfNecessary(nextContent.Count);

		for(int i = 0; i < nextContentImages.Count; i++)
		{
			if (i < nextContent.Count)
			{
				nextContentImages[i].enabled = true;
				
				if (nextContent[i].contentType == TileContent.Ice)
				{
					nextContentImages[i].sprite = tileContentSprites.IceSprite;
				}
				else if (nextContent[i].contentType == TileContent.Fruit)
				{
					nextContentImages[i].sprite = tileContentSprites.FruitSprites[nextContent[i].fruitID];
				}
			}
			else
			{
				nextContentImages[i].enabled = false;
			}
		}
	}

	private void CreateNewNextContentImagesIfNecessary(int nextContentCount)
	{
		int originalNextContentImagesCount = nextContentImages.Count;

		if (nextContentCount > originalNextContentImagesCount)
		{
			for (int i = 0; i < nextContentCount - originalNextContentImagesCount; i++)
			{
				Image newImage = Instantiate(nextContentImagePrefab, nextContentImagesParent).GetComponent<Image>();
				nextContentImages.Add(newImage);
			}
		}
	}

	private void ShowGameOverWindow(int points, int turn)
	{
		gameOverWindow.SetActive(true);
		gameOverText.text = Modules.Localization.GetLocalizedText(GAME_OVER_LOCALIZATION_KEY, points.ToString(), turn.ToString());
	}
}
