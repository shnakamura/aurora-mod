using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace Aurora.Common.Materials;

[Autoload(Side = ModSide.Client)]
public sealed class MaterialSystem : ModSystem
{
    private static Dictionary<int, string> materials = new();

    public override void PostSetupContent() {
        base.PostSetupContent();
        
        foreach (var tile in ModContent.GetContent<ModTile>()) {
            var type = tile.GetType();
            
            var attribute = type.GetCustomAttribute<MaterialAttribute>();

            if (attribute == null) {
                continue;
            }

            materials[tile.Type] = attribute.Name;
        }
        
        PopulateMaterials();
    }

    public override void Unload() {
        base.Unload();
        
        materials?.Clear();
        materials = null;
    }

    public static void RegisterMaterial(int tileType, string materialName) {
        materials[tileType] = materialName;
    }

    public static bool TryGetMaterial(int tileType, out string materialName) {
        return materials.TryGetValue(tileType, out materialName);
    }

    private static void PopulateMaterials() {
        for (int i = 0; i < TileLoader.TileCount; i++) {
            if (TileID.Sets.Grass[i]) {
                RegisterMaterial(i, "Grass");
            }
            else if (TileID.Sets.Dirt[i]) {
                RegisterMaterial(i, "Dirt");
            }
            else if (TileID.Sets.Snow[i]) {
                RegisterMaterial(i, "Snow");
            }
            else if (TileID.Sets.Stone[i]) {
                RegisterMaterial(i, "Stone");
            }
        }
    }
}
