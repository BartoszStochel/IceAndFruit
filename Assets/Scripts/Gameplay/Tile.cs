using UnityEngine;
using UnityEngine.UI;
using System;

public class Tile : MonoBehaviour, IPathTile
{
	private const string ANIMATOR_SHOW_TRIGGER_NAME = "Show";
	private const string ANIMATOR_SHOW_STATE_NAME = "TileContentShowAnimation";
	private const string ANIMATOR_HIDE_TRIGGER_NAME = "Hide";
	private const string ANIMATOR_HIDE_STATE_NAME = "TileContentHideAnimation";

#pragma warning disable 0649
	[SerializeField] private TileContentSprites tileContentSprites;
	[SerializeField] private Image tileContentImage;
	[SerializeField] private Animator tileContentAnimator;
#pragma warning restore 0649

	public int XPosition { get; private set; }
	public int YPosition { get; private set; }
	public TileContent TileContent { get; private set; }
	public int FruitId { get; private set; }

	public event Action<Tile> TileHasBeenClicked;
	
	public void Initialize(int xPosition, int yPosition)
	{
		XPosition = xPosition;
		YPosition = yPosition;

		name = $"Tile {xPosition}, {yPosition}";

		SetPositionOnCanvas();
	}

	public void SetContent(TileContent newContent, int fruitId = -1, bool immediately = false)
	{
		TileContent = newContent;
		FruitId = fruitId;

		if (TileContent == TileContent.None)
		{
			HandleContentAnimations(ANIMATOR_HIDE_STATE_NAME, ANIMATOR_HIDE_TRIGGER_NAME, immediately);

			return;
		}

		HandleContentAnimations(ANIMATOR_SHOW_STATE_NAME, ANIMATOR_SHOW_TRIGGER_NAME, immediately);

		if (TileContent == TileContent.Ice)
		{
			tileContentImage.sprite = tileContentSprites.IceSprite;
			return;
		}

		if (TileContent == TileContent.Fruit)
		{
			if (FruitId < tileContentSprites.FruitSprites.Count)
			{
				tileContentImage.sprite = tileContentSprites.FruitSprites[FruitId];
			}
			else
			{
				Debug.LogError("Not enough fruit sprites!");
			}
		}
	}

	// Used by button in scene.
	public void TileClicked()
	{
		TileHasBeenClicked?.Invoke(this);
	}

	public bool CanPathGoThroughTile()
	{
		return TileContent == TileContent.None;
	}

	private void HandleContentAnimations(string stateName, string triggerName, bool immediately)
	{
		tileContentAnimator.ResetTrigger(ANIMATOR_HIDE_TRIGGER_NAME);
		tileContentAnimator.ResetTrigger(ANIMATOR_SHOW_TRIGGER_NAME);

		if (immediately)
		{
			tileContentAnimator.Play(stateName, -1, 1f);
		}
		else
		{
			tileContentAnimator.SetTrigger(triggerName);
		}
	}

	private void SetPositionOnCanvas()
	{
		RectTransform rectTransform = GetComponent<RectTransform>();

		if (rectTransform == null)
		{
			Debug.LogError("RectTransform of Tile could not be found!");
			return;
		}

		float tileWidth = rectTransform.sizeDelta.x;
		float tileHeight = rectTransform.sizeDelta.y;

		float boardWidth = tileWidth * Modules.Balance.BoardXSize;
		float boardHeight = tileHeight * Modules.Balance.BoardYSize;

		rectTransform.anchoredPosition =
			new Vector2(
				tileWidth * XPosition - boardWidth / 2f,
				tileHeight * YPosition - boardHeight / 2f);
	}
}
