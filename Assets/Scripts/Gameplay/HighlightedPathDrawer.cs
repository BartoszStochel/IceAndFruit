using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightedPathDrawer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private GameObject highlightedElementPrefab;
	[SerializeField] private float highlightTime;
	[SerializeField] private Board board;
#pragma warning restore 0649

	private List<Image> highlightedElements = new List<Image>();
	private Coroutine hidingCoroutine;
	private WaitForSeconds delayBeforeHide;

	private void Awake()
	{
		board.Initialized += Initialize;
	}

	private void Initialize()
	{
		board.BoardActions.NewPathCreated += DrawPath;
		delayBeforeHide = new WaitForSeconds(highlightTime);
	}

	public void DrawPath(PathElement lastElementOfPath, Tile[,] tiles)
	{
		if (hidingCoroutine != null)
		{
			StopCoroutine(hidingCoroutine);
		}

		CreateNewPathElementsIfNecessary(lastElementOfPath);
		HideAllElements();

		PathElement currentPathElement = lastElementOfPath;

		int highlightedElementIndex = 0;
		while(currentPathElement != null)
		{
			highlightedElements[highlightedElementIndex].enabled = true;
			highlightedElements[highlightedElementIndex].transform.position =
				tiles[currentPathElement.Tile.XPosition, currentPathElement.Tile.YPosition].transform.position;

			highlightedElementIndex++;
			currentPathElement = currentPathElement.PreviousElement;
		}

		hidingCoroutine = StartCoroutine(HideHighlightedElementsAfterDelay());
	}

	private void CreateNewPathElementsIfNecessary(PathElement path)
	{
		int pathLength = path.GetPathLength();
		int howManyNewElementsAreNeeded = pathLength - highlightedElements.Count;

		for (int i = 0; i < howManyNewElementsAreNeeded; i++)
		{
			GameObject newHighlightedElement = Instantiate(highlightedElementPrefab, transform);
			highlightedElements.Add(newHighlightedElement.GetComponent<Image>());
		}		
	}

	private IEnumerator HideHighlightedElementsAfterDelay()
	{
		yield return delayBeforeHide;

		HideAllElements();
	}

	private void HideAllElements()
	{
		for (int i = 0; i < highlightedElements.Count; i++)
		{
			highlightedElements[i].enabled = false;
		}
	}
}
