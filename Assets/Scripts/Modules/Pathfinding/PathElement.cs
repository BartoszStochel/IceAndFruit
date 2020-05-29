public class PathElement
{
	public IPathTile Tile { get; private set; }
	public PathElement PreviousElement { get; private set; }

	public PathElement(IPathTile tile, PathElement previousElement = null)
	{
		Tile = tile;
		PreviousElement = previousElement;
	}

	public int GetPathLength()
	{
		int length = 1;

		if (PreviousElement != null)
		{
			length += PreviousElement.GetPathLength();
		}

		return length;
	}
}