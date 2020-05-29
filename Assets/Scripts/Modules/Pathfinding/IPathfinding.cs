public interface IPathfinding
{
	PathElement GetPathEndAtDestination(IPathTile from, IPathTile destination, IPathTile[,] tiles);
}
