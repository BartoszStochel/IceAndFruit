using System.Collections.Generic;

public interface ICombinationChecker
{
	List<CombinationData> CheckForCombinationsInLine(List<Tile> lineToCheck);
}
