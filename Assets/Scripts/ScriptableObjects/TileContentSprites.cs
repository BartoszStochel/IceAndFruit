using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitSprites", menuName = "Configuration/Fruit sprites container")]
public class TileContentSprites : ScriptableObject
{
#pragma warning disable 0649
	[SerializeField] private List<Sprite> fruitSprites;
	[SerializeField] private Sprite iceSprite;
#pragma warning restore 0649

	public List<Sprite> FruitSprites => fruitSprites;
	public Sprite IceSprite => iceSprite;
}
