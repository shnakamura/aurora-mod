namespace Aurora.Utilities;

/// <summary>
///		Provides <see cref="Tile"/> extensions.
/// </summary>
public static class TileExtensions
{
	/// <summary>
	///		Checks whether a tile has an active wall or not.
	/// </summary>
	/// <param name="tile">The tile to check.</param>
	/// <returns><c>true</c> if the tile has a wall; otherwise, <c>false</c>.</returns>
	public static bool HasWall(this Tile tile) {
		return tile.WallType == 0;
	}
}
