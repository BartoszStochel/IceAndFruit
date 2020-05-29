using UnityEngine;

public class Balance : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private int boardXSize;
	[SerializeField] private int boardYSize;
	[SerializeField] private int numberOfFruitsUsed;
	[SerializeField] private int portionsOfContentOnBoardOnStart;
	[SerializeField] private int iceCubesInOnePortionOfContent;
	[SerializeField] private int fruitsInOnePortionOfContent;
	[SerializeField] private int pointsForThreeDifferentFruitsCombination;
	[SerializeField] private int pointsForOneFruitInSameFruitCombination;
#pragma warning restore 0649

	public int BoardXSize => boardXSize;
	public int BoardYSize => boardYSize;
	public int NumberOfFruitsUsed => numberOfFruitsUsed;
	public int PortionsOfContentOnBoardOnStart => portionsOfContentOnBoardOnStart;
	public int IceCubesInOnePortionOfContent => iceCubesInOnePortionOfContent;
	public int FruitsInOnePortionOfContent => fruitsInOnePortionOfContent;
	public int PointsForThreeDifferentFruitsCombination => pointsForThreeDifferentFruitsCombination;
	public int PointsForOneFruitInSameFruitCombination => pointsForOneFruitInSameFruitCombination;
}
