using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace Aurora.Common.Materials;

/// <summary>
///		Handles registration and loading of tile materials through <see cref="MaterialAttribute"/> for
///		modded tiles and manual callbacks for vanilla tiles.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class TileMaterialSystem : ModSystem
{
	private static Dictionary<int, string>? materialsByName = [];

    public override void PostSetupContent() {
        base.PostSetupContent();
        
        LoadModdedMaterials();
        LoadVanillaMaterials();
    }

    public override void Unload() {
        base.Unload();
        
        materialsByName?.Clear();
        materialsByName = null;
    }
    
    /// <summary>
    ///		Registers a tile material from a type set.
    /// </summary>
    /// <param name="materialName">The name of the material.</param>
    /// <param name="tileTypes">The types associated with the material.</param>
    public static void RegisterMaterial(string materialName, params int[] tileTypes) {
	    for (int i = 0; i < tileTypes.Length; i++) {
		    materialsByName[tileTypes[i]] = materialName;
	    }
    }
    
    /// <summary>
    ///		Registers a tile material from a content set.
    /// </summary>
    /// <param name="materialName">The name of the material.</param>
    /// <param name="set">The factory set associated with the material.</param>
    public static void RegisterMaterial(string materialName, bool[] set) {
	    for (int i = 0; i < set.Length; i++) {
		    if (set[i]) {
			    RegisterMaterial(materialName, i);
		    }
	    }
    }

    /// <summary>
    ///		Attempts to retrieve a material from a tile type.
    /// </summary>
    /// <param name="tileType">The type of the tile.</param>
    /// <param name="materialName">The name of the material retrieved.</param>
    /// <returns><c>true</c> if a material was successfully retrieved; otherwise, <c>false</c>.</returns>
    public static bool TryGetMaterial(int tileType, out string materialName) {
        return materialsByName.TryGetValue(tileType, out materialName);
    }
    
    /// <summary>
    ///		Attempts to retrieve a material from a tile.
    /// </summary>
    /// <param name="tileType">The type of the tile.</param>
    /// <param name="materialName">The name of the material retrieved.</param>
    /// <returns><c>true</c> if a material was successfully retrieved; otherwise, <c>false</c>.</returns>
    public static bool TryGetMaterial(Tile tile, out string materialName) {
	    return TryGetMaterial(tile.TileType, out materialName);
    }

    private static void LoadModdedMaterials() {
	    foreach (var tile in ModContent.GetContent<ModTile>()) {
		    var type = tile.GetType();
            
		    var attribute = type.GetCustomAttribute<MaterialAttribute>();

		    if (attribute == null) {
			    continue;
		    }

		    materialsByName[tile.Type] = attribute.Name;
	    }
    }

    private static void LoadVanillaMaterials() {
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
}
