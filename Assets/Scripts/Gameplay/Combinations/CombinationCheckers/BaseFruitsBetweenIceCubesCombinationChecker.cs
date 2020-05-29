using System.Collections.Generic;

public abstract class BaseFruitsBetweenIceCubesCombinationChecker : BaseCombinationChecker
{
	public override List<CombinationData> CheckForCombinationsInLine(List<Tile> lineToCheck)
	{
		List<Tile> iceCubes = lineToCheck.FindAll(t => t.TileContent == TileContent.Ice);
		List<List<Tile>> tilesPortionsToCheck = GetTilesEnclosedInIceCubes(iceCubes, lineToCheck);

		return CheckAllTilesPortions(tilesPortionsToCheck);
	}

	protected bool AllTilesAreFruits(List<Tile> tiles)
	{
		return tiles.TrueForAll(t => t.TileContent == TileContent.Fruit);
	}

	protected abstract bool AreCombinationConditionsMet(List<Tile> tilesWithoutIceCubes);

	private List<List<Tile>> GetTilesEnclosedInIceCubes(List<Tile> iceCubes, List<Tile> lineToCheck)
	{
		List<List<Tile>> tilesPortions = new List<List<Tile>>();

		for (int i = 0; i < iceCubes.Count - 1; i++)
		{
			Tile currentIceCube = iceCubes[i];
			Tile nextIceCube = iceCubes[i + 1];

			int indexOfCurrentIceCube = lineToCheck.IndexOf(currentIceCube);
			int indexOfNextIceCube = lineToCheck.IndexOf(nextIceCube);

			List<Tile> tilesPortion = lineToCheck.GetRange(indexOfCurrentIceCube, indexOfNextIceCube - indexOfCurrentIceCube + 1);
			tilesPortions.Add(tilesPortion);
		}

		return tilesPortions;
	}

	private List<CombinationData> CheckAllTilesPortions(List<List<Tile>> tilesPortionsToCheck)
	{
		List<CombinationData> foundCombinations = new List<CombinationData>();

		for (int i = 0; i < tilesPortionsToCheck.Count; i++)
		{
			List<Tile> tilesWithoutIceCubes = GetPortionWithoutIceCubes(tilesPortionsToCheck[i]);

			if (AreCombinationConditionsMet(tilesWithoutIceCubes))
			{
				foundCombinations.Add(new CombinationData(CheckedCombinationType, tilesPortionsToCheck[i], GetScoreForCombination(tilesWithoutIceCubes)));
			}
		}

		return foundCombinations;
	}

	private List<Tile> GetPortionWithoutIceCubes(List<Tile> tilesPortion)
	{
		List<Tile> portionWithoutIceCubes = new List<Tile>(tilesPortion);

		portionWithoutIceCubes.RemoveAll(t => t.TileContent == TileContent.Ice);

		return portionWithoutIceCubes;
	}
}
