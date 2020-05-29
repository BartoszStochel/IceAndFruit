using System.Collections.Generic;

public class BoardCombinations
{
	private Tile[,] tiles;
	private List<ICombinationChecker> combinationCheckers;

    public BoardCombinations(Tile [,] newTiles)
	{
		tiles = newTiles;
		InitializeCombinationCheckersInParticularOrder();
	}

	public bool IsThereAnyCombination(Tile movedTile)
	{
		return GetCombinationsOnBoard(movedTile).Count > 0;
	}

	public List<CombinationData> GetCombinationsOnBoard(Tile movedTile)
	{
		List<CombinationData> foundCombinations = new List<CombinationData>();

		List<CombinationData> horizontalCombinations = CheckForCombinationsInOneDimension(0, movedTile.YPosition);
		List<CombinationData> verticalCombinations = CheckForCombinationsInOneDimension(1, movedTile.XPosition);

		foundCombinations.AddRange(horizontalCombinations);
		foundCombinations.AddRange(verticalCombinations);

		return foundCombinations;
	}

	private List<CombinationData> CheckForCombinationsInOneDimension(int dimensionIndexInArray, int movedTilePositionInAnotherDimension)
	{
		List<Tile> line = tiles.GetOneLineAsList(dimensionIndexInArray, movedTilePositionInAnotherDimension);

		List<CombinationData> foundCombinations = new List<CombinationData>();

		for (int i = 0; i < combinationCheckers.Count; i++)
		{
			foundCombinations.AddRange(combinationCheckers[i].CheckForCombinationsInLine(line));
		}

		return foundCombinations;
	}

	private void InitializeCombinationCheckersInParticularOrder()
	{
		combinationCheckers = new List<ICombinationChecker>();

		combinationCheckers.Add(new ThreeDifferentFruitsChecker());
		combinationCheckers.Add(new AllIdenticalFruitsChecker());
	}
}
