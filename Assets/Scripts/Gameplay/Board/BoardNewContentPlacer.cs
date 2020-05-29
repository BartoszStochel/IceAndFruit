using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class BoardNewContentPlacer
{
	public event Action BoardIsFull;
	public event Action<List<(TileContent contentType, int fruitID)>> NewContentToPlaceIsChosen;

	private Tile[,] tiles;
	private BoardCombinations boardCombinations;
	private List<(TileContent contentType, int fruitID)> nextContentToPlace = new List<(TileContent contentType, int fruitID)>();

	public BoardNewContentPlacer(Tile[,] newTiles, BoardCombinations newBoardCombinations)
	{
		tiles = newTiles;
		boardCombinations = newBoardCombinations;
	}

	public bool TryToPlaceNewPortionOfContentOnBoard()
	{
		List<Tile> tilesWithNoContent = tiles.TransformToSingleList();
		tilesWithNoContent.RemoveAll(t => t.TileContent != TileContent.None);

		for (int i = 0; i < nextContentToPlace.Count; i++)
		{
			TryToPlaceOnePieceOfContent(nextContentToPlace[i].contentType, nextContentToPlace[i].fruitID, tilesWithNoContent);
		}

		if (tilesWithNoContent.Count == 0)
		{
			BoardIsFull?.Invoke();
			return false;
		}
		else
		{
			ChooseNextContentToPlace();
			return true;
		}
	}

	public void PlaceInitialBoardContent()
	{
		foreach (Tile tile in tiles)
		{
			tile.SetContent(TileContent.None, -1, true);
		}

		ChooseNextContentToPlace();

		for (int i = 0; i < Modules.Balance.PortionsOfContentOnBoardOnStart; i++)
		{
			TryToPlaceNewPortionOfContentOnBoard();
		}
	}

	private void TryToPlaceOnePieceOfContent(TileContent type, int fruitID, List<Tile> tilesWithNoContent)
	{
		List<Tile> tilesAvailableForThisContent = new List<Tile>(tilesWithNoContent);
		Tile newTile = null;
		bool thereIsACombinationOnBoardAfterPlacementOfConent;

		do
		{
			if (tilesAvailableForThisContent.Count == 0)
			{
				return;
			}

			newTile = tilesAvailableForThisContent[Random.Range(0, tilesAvailableForThisContent.Count)];
			newTile.SetContent(type, fruitID);

			thereIsACombinationOnBoardAfterPlacementOfConent = boardCombinations.IsThereAnyCombination(newTile);

			if (thereIsACombinationOnBoardAfterPlacementOfConent)
			{
				newTile.SetContent(TileContent.None, -1, true);
				tilesAvailableForThisContent.Remove(newTile);
			}
		}
		while (thereIsACombinationOnBoardAfterPlacementOfConent);

		tilesWithNoContent.Remove(newTile);
	}

	private void ChooseNextContentToPlace()
	{
		int alreadyChosenContentCount = 0;

		ChooseContentOfOneType(TileContent.Ice, Modules.Balance.IceCubesInOnePortionOfContent, ref alreadyChosenContentCount);
		ChooseContentOfOneType(TileContent.Fruit, Modules.Balance.FruitsInOnePortionOfContent, ref alreadyChosenContentCount);

		if (alreadyChosenContentCount < nextContentToPlace.Count)
		{
			nextContentToPlace.RemoveRange(alreadyChosenContentCount, nextContentToPlace.Count - alreadyChosenContentCount);
		}

		NewContentToPlaceIsChosen?.Invoke(nextContentToPlace);
	}

	private void ChooseContentOfOneType(TileContent type, int numberOfTilesOfThisType, ref int alreadyChosenContentCount)
	{
		for (int i = alreadyChosenContentCount; i < alreadyChosenContentCount + numberOfTilesOfThisType; i++)
		{
			if (i == nextContentToPlace.Count)
			{
				nextContentToPlace.Add((TileContent.None, 0));
			}

			nextContentToPlace[i] = (type, Random.Range(0, Modules.Balance.NumberOfFruitsUsed));
		}

		alreadyChosenContentCount += numberOfTilesOfThisType;
	}
}
