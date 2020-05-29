using System;

public class BoardActions
{
	public event Action<Tile> NewTileIsSelected;
	public event Action<Tile> TileSuccessfullyMoved;
	public event Action<PathElement, Tile[,]> NewPathCreated;

	private Tile[,] tiles;
	private Tile selectedTile;

	public BoardActions(Tile[,] newTiles)
	{
		tiles = newTiles;

		foreach (Tile tile in tiles)
		{
			tile.TileHasBeenClicked += TileClicked;
		}
	}

	private void TileClicked(Tile clickedTile)
	{
		// If there is no selected tile - select clicked tile.
		if (selectedTile == null)
		{
			SelectTile(clickedTile);
			return;
		}

		// If selected tile was clicked again - deselect it.
		if (selectedTile == clickedTile)
		{
			SelectTile(null);
			return;
		}

		PathElement pathFromDestination = Modules.Pathfinding.GetPathEndAtDestination(selectedTile, clickedTile, tiles);

		// If selected tile can not be moved to clicked tile - select clicked tile.
		if (pathFromDestination == null)
		{
			SelectTile(clickedTile);
			return;
		}

		// If selected tile is successfully moved to clicked tile - deselect it.
		clickedTile.SetContent(selectedTile.TileContent, selectedTile.FruitId, true);
		selectedTile.SetContent(TileContent.None, -1, true);

		SelectTile(null);

		TileSuccessfullyMoved?.Invoke(clickedTile);
		NewPathCreated?.Invoke(pathFromDestination, tiles);
	}

	private void SelectTile(Tile tile)
	{
		// If selected tile has no content - do not select it
		if (tile != null && tile.TileContent == TileContent.None)
		{
			selectedTile = null;
		}
		else
		{
			selectedTile = tile;
		}

		NewTileIsSelected?.Invoke(selectedTile);
	}
}
