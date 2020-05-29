using System.Collections.Generic;

public class ThreeDifferentFruitsChecker : BaseFruitsBetweenIceCubesCombinationChecker, ICombinationChecker
{
	protected override CombinationType CheckedCombinationType => CombinationType.ThreeDifferentFruits;

	protected override bool AreCombinationConditionsMet(List<Tile> tilesWithoutIceCubes)
	{
		return
			PortionIsThreeElementsLong(tilesWithoutIceCubes) &&
			AllTilesAreFruits(tilesWithoutIceCubes) &&
			AllFruitsHaveDifferentIds(tilesWithoutIceCubes);
	}

	protected override int GetScoreForCombination(List<Tile> tilesToCheck)
	{
		return Modules.Balance.PointsForThreeDifferentFruitsCombination;
	}

	private bool PortionIsThreeElementsLong(List<Tile> fruits)
	{
		return fruits.Count == 3;
	}

	private bool AllFruitsHaveDifferentIds(List<Tile> fruits)
	{
		return 
			fruits[0].FruitId != fruits[1].FruitId &&
			fruits[0].FruitId != fruits[2].FruitId &&
			fruits[1].FruitId != fruits[2].FruitId;
	}
}
