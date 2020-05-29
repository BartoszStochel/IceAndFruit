using System.Collections.Generic;

public abstract class BaseCombinationChecker : ICombinationChecker
{
	protected abstract CombinationType CheckedCombinationType { get; }

	public abstract List<CombinationData> CheckForCombinationsInLine(List<Tile> lineToCheck);

	protected abstract int GetScoreForCombination(List<Tile> tilesToCheck);
}
