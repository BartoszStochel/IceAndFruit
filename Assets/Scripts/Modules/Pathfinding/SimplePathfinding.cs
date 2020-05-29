using System.Collections.Generic;

// Unoptimized, but gets the work done.
// It chcecks once checked tiles once again. Minor thing, really.
public class SimplePathfinding : IPathfinding
{
	public PathElement GetPathEndAtDestination(IPathTile from, IPathTile destination, IPathTile[,] tiles)
	{
		if (from == destination)
		{
			return new PathElement(from);
		}

		if (!destination.CanPathGoThroughTile())
		{
			return null;
		}

		List<PathElement> checkAdjacentTilesOfTheseTilesNow = new List<PathElement> { new PathElement(from) };

		for (int i = 0; i < checkAdjacentTilesOfTheseTilesNow.Count; i++)
		{
			List<PathElement> adjacentTiles = GetAdjacentTilesOfTile(checkAdjacentTilesOfTheseTilesNow[i], tiles);

			for (int j = 0; j < adjacentTiles.Count; j++)
			{
				if (adjacentTiles[j].Tile == destination)
				{
					return adjacentTiles[j];
				}

				if (adjacentTiles[j].Tile.CanPathGoThroughTile() && !HasTileBeenChecked(adjacentTiles[j], checkAdjacentTilesOfTheseTilesNow))
				{
					checkAdjacentTilesOfTheseTilesNow.Add(adjacentTiles[j]);
				}
			}
		}
		
		return null;
	}

	private static List<PathElement> GetAdjacentTilesOfTile(PathElement tile, IPathTile[,] tiles)
	{
		List<PathElement> adjacentTiles = new List<PathElement>();

		// Up tile.
		if (tile.Tile.YPosition < Modules.Balance.BoardYSize - 1)
		{
			adjacentTiles.Add(new PathElement(tiles[tile.Tile.XPosition, tile.Tile.YPosition + 1], tile));
		}

		// Right tile.
		if (tile.Tile.XPosition < Modules.Balance.BoardXSize - 1)
		{
			adjacentTiles.Add(new PathElement(tiles[tile.Tile.XPosition + 1, tile.Tile.YPosition], tile));
		}

		// Down tile.
		if (tile.Tile.YPosition > 0)
		{
			adjacentTiles.Add(new PathElement(tiles[tile.Tile.XPosition, tile.Tile.YPosition - 1], tile));
		}

		// Left tile.
		if (tile.Tile.XPosition > 0)
		{
			adjacentTiles.Add(new PathElement(tiles[tile.Tile.XPosition - 1, tile.Tile.YPosition], tile));
		}

		return adjacentTiles;
	}

	private bool HasTileBeenChecked(PathElement tile, List<PathElement> alreadyCheckedTiles)
	{
		for (int i = 0; i < alreadyCheckedTiles.Count; i++)
		{
			if (alreadyCheckedTiles[i].Tile == tile.Tile)
			{
				return true;
			}
		}

		return false;
	}
}
