using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace Aurora.Common.Materials;

[Autoload(Side = ModSide.Client)]
public sealed class TileMaterialSystem : ModSystem
{
    private static Dictionary<int, string>? materials = new();

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
        
        RegisterMaterial(
	        "Grass",
	        TileID.LivingMahoganyLeaves
	    );
        
        RegisterMaterial("Grass", TileID.Sets.Grass);
        
        RegisterMaterial(
            "Stone", 
            TileID.GrayBrick, 
            TileID.StoneSlab, 
            TileID.Mudstone
        );

        RegisterMaterial("Stone", TileID.Sets.Stone);
      
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
    }

    public override void Unload() {
        base.Unload();
        
        materials?.Clear();
        materials = null;
    }

    public static bool TryGetMaterial(int tileType, out string materialName) {
        return materials.TryGetValue(tileType, out materialName);
    }
    
    public static bool TryGetMaterial(Tile tile, out string materialName) {
	    return TryGetMaterial(tile.TileType, out materialName);
    }

    public static void RegisterMaterial(string materialName, params int[] tileTypes) {
        for (int i = 0; i < tileTypes.Length; i++) {
            materials[tileTypes[i]] = materialName;
        }
    }
    
    private static void RegisterMaterial(string materialName, bool[] set) {
        for (int i = 0; i < set.Length; i++) {
            if (set[i]) {
                RegisterMaterial(materialName, i);
            }
        }
    }
}
