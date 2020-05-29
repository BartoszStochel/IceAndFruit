using System.Collections.Generic;

public class CombinationData
{
    public CombinationType Combination { get; private set; }
    public List<Tile> AssociatedTiles { get; private set; }
	public int Score { get; private set; }

	public CombinationData(CombinationType combination, List<Tile> associatedTiles, int score)
	{
		Combination = combination;
		AssociatedTiles = associatedTiles;
		Score = score;
	}
}
