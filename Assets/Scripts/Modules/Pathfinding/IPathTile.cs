public interface IPathTile
{
	int XPosition { get; }
	int YPosition { get; }

	bool CanPathGoThroughTile();
}
