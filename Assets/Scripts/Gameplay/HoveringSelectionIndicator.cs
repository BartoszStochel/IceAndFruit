using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class HoveringSelectionIndicator : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private Board board;
#pragma warning restore 0649

	private RectTransform rectTransform;
	private Image image;

	private void Awake()
	{
		board.Initialized += Initialize;
	}

	private void Initialize()
	{
		board.BoardActions.NewTileIsSelected += ShowSelectionOnTile;

		rectTransform = GetComponent<RectTransform>();
		image = GetComponent<Image>();

		image.enabled = false;
	}

	public void ShowSelectionOnTile(Tile tile)
	{
		if (tile == null)
		{
			image.enabled = false;
			return;
		}

		image.enabled = true;

		RectTransform rectTransformOfTile = tile.GetComponent<RectTransform>();

		if (rectTransformOfTile == null)
		{
			Debug.LogError("Could not get RectTransform component!");
			return;
		}

		rectTransform.position = rectTransformOfTile.position;
	}
}
