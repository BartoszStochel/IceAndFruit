using System.Collections.Generic;

public class AllIdenticalFruitsChecker : BaseFruitsBetweenIceCubesCombinationChecker
{
	protected override CombinationType CheckedCombinationType => CombinationType.AllIdenticalFruits;

	protected override bool AreCombinationConditionsMet(List<Tile> tilesWithoutIceCubes)
	{
		return
			PortionIsAtLeastThreeElementsLong(tilesWithoutIceCubes) &&
			AllTilesAreFruits(tilesWithoutIceCubes) &&
			AllFruitsHaveIdenticalIds(tilesWithoutIceCubes);
	}

	protected override int GetScoreForCombination(List<Tile> tilesToCheck)
	{
		return Modules.Balance.PointsForOneFruitInSameFruitCombination * tilesToCheck.Count;
	}

	private bool PortionIsAtLeastThreeElementsLong(List<Tile> fruits)
	{
		return fruits.Count >= 3;
	}

	private bool AllFruitsHaveIdenticalIds(List<Tile> fruits)
	{
		return fruits.TrueForAll(f => f.FruitId == fruits[0].FruitId);
	}
}
