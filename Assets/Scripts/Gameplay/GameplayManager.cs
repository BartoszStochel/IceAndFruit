using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private Board board;
#pragma warning restore 0649

	public Board Board => board;

	public event Action Initialized;
	public delegate void TurnInfoDelegate(int points, int turnNumber);
	public event TurnInfoDelegate GameEnded;
	public event TurnInfoDelegate NewTurnHasBegun;

	private int currentScore;
	private int numberOfMovesMade;

	private void Start()
	{
		board.Initialize();
		board.BoardActions.TileSuccessfullyMoved += ProceedToNextTurn;
		board.BoardNewContentPlacer.BoardIsFull += EndGame;

		Initialized?.Invoke();
		StartGame();
	}

	public void GoBackToMainMenu()
	{
		SceneManager.LoadScene(Modules.SceneNames.MainMenuSceneName);
	}

	private void ProceedToNextTurn(Tile movedTile)
	{
		numberOfMovesMade++;

		bool wereAnyCombinationsFound = AssignScoreForCombinationsFoundOnBoard(movedTile);

		if (!wereAnyCombinationsFound)
		{
			board.BoardNewContentPlacer.TryToPlaceNewPortionOfContentOnBoard();
		}

		NewTurnHasBegun?.Invoke(currentScore, numberOfMovesMade);
	}

	private bool AssignScoreForCombinationsFoundOnBoard(Tile movedTile)
	{
		List<CombinationData> foundCombinations = board.BoardCombinations.GetCombinationsOnBoard(movedTile);

		for (int i = 0; i < foundCombinations.Count; i++)
		{
			currentScore += foundCombinations[i].Score;
			foundCombinations[i].AssociatedTiles.ForEach(t => t.SetContent(TileContent.None));
		}

		return foundCombinations.Count > 0;
	}

	private void StartGame()
	{
		board.BoardNewContentPlacer.PlaceInitialBoardContent();
	}

	private void EndGame()
	{
		GameEnded?.Invoke(currentScore, numberOfMovesMade);
	}
}
