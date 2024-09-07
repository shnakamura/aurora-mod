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
        
        RegisterMaterial("Grass", TileID.LivingMahoganyLeaves);
        
        RegisterMaterial(
            "Stone", 
            TileID.GrayBrick, 
            TileID.StoneSlab, 
            TileID.Mudstone
        );
      
        RegisterMaterial(
            "Wood", 
            TileID.Platforms,
            TileID.WoodBlock,
            TileID.AshWood,
            TileID.Shadewood,
            TileID.Pearlwood,
            TileID.BorealWood,
            TileID.LivingWood,
            TileID.DynastyWood,
            TileID.Ebonwood,
            TileID.SpookyWood
        );
        
        RegisterMaterialFromSet(TileID.Sets.Grass, "Grass");
        RegisterMaterialFromSet(TileID.Sets.Conversion.Grass, "Grass");
        
        RegisterMaterialFromSet(TileID.Sets.Stone, "Stone");
        RegisterMaterialFromSet(TileID.Sets.Conversion.Stone, "Stone");
        
        RegisterMaterialFromSet(TileID.Sets.CrackedBricks, "Stone");
    }

    public override void Unload() {
        base.Unload();
        
        materials?.Clear();
        materials = null;
    }

    public static bool TryGetMaterial(int tileType, out string materialName) {
        return materials.TryGetValue(tileType, out materialName);
    }

    public static void RegisterMaterial(string materialName, params int[] tileTypes) {
        for (int i = 0; i < tileTypes.Length; i++) {
            materials[tileTypes[i]] = materialName;
        }
    }
    
    private static void RegisterMaterialFromSet(bool[] set, string materialName) {
        for (int i = 0; i < set.Length; i++) {
            if (set[i]) {
                RegisterMaterial(materialName, i);
            }
        }
    }
}
