using Aurora.Common.Materials;

namespace Aurora.Utilities;

public static class TileExtensions
{
    public static bool TryGetMaterial(this Tile tile, out string materialName) {
        return MaterialSystem.TryGetMaterial(tile.TileType, out materialName);
    }
}
