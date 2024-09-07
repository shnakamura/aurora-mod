using Aurora.Common.Materials;

namespace Aurora.Utilities;

/// <summary>
///		Provides <see cref="Tile"/> extensions.
/// </summary>
public static class TileExtensions
{
    public static bool TryGetMaterial(this Tile tile, out string materialName) {
        return MaterialSystem.TryGetMaterial(tile.TileType, out materialName);
    }
}
