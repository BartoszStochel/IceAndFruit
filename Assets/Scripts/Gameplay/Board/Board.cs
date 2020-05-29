using UnityEngine;
using System;

public class Board : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private Tile tilePrefab;
#pragma warning restore 0649

	public BoardActions BoardActions { get; private set; }
	public BoardCombinations BoardCombinations { get; private set; }
	public BoardNewContentPlacer BoardNewContentPlacer { get; private set; }

	public event Action Initialized;

	private Tile[,] tiles;

	public void Initialize()
	{
		CreateTiles();
		BoardActions = new BoardActions(tiles);
		BoardCombinations = new BoardCombinations(tiles);
		BoardNewContentPlacer = new BoardNewContentPlacer(tiles, BoardCombinations);

		Initialized?.Invoke();
	}

	private void CreateTiles()
	{
		tiles = new Tile[Modules.Balance.BoardXSize, Modules.Balance.BoardYSize];

		for (int i = 0; i < Modules.Balance.BoardXSize; i++)
		{
			for (int j = 0; j < Modules.Balance.BoardYSize; j++)
			{
				Tile instantiatedTile = Instantiate(tilePrefab, transform);

				instantiatedTile.Initialize(i, j);
				tiles[i, j] = instantiatedTile;
			}
		}
	}
}
