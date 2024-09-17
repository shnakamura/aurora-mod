using System.Collections.Generic;
using Aurora.Utilities;
using StructureHelper;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Aurora.Common.World;

public sealed class TundraRuinsMicroBiome : MicroBiome
{
	private const string StructurePath = "Assets/Structures/TundraRuins0";
	
	public override bool Place(Point origin, StructureMap structures) {
		var mod = Aurora.Instance;
		var dims = Point16.Zero;

		if (!Generator.GetDimensions(StructurePath, mod, ref dims)) {
			return false;
		}

		var position = origin - new Point(dims.X / 2, dims.Y - dims.Y / 3);
		var area = new Rectangle(position.X, position.Y, dims.X, dims.Y);
		
		if (!structures.CanPlace(area)) {
			return false;
		}
		
		var scan = new Rectangle(position.X - 20, position.Y, dims.X + 20, dims.Y);

		var terrainCount = 0;
		var airCount = 0;

		var threshold = 10;
		
		var terrainAmount = dims.X * dims.Y / 3 - threshold;
		var airAmount = dims.X * dims.Y - terrainAmount - threshold;

		if (airCount < airAmount || terrainCount < terrainAmount) {
			return false;
		}
		
		structures.AddProtectedStructure(area);
		
		Generator.GenerateStructure(StructurePath, new Point16(position), mod);
		
		// Fills up a blotch to make the structure naturally blend within the pre-existing terrain.
		for (var i = 0; i < 5; i++) {
			var strength = WorldGen.genRand.Next(8, 13);
			var steps = WorldGen.genRand.Next(2, 5);

			WorldGen.TileRunner(position.X - i, position.Y + dims.Y - i, strength, steps, TileID.SnowBlock, true, overRide: false);
			WorldGen.TileRunner(position.X + dims.X + i, position.Y + dims.Y - i, strength, steps, TileID.SnowBlock, true, overRide: false);
		}
		
		return true;
	}
}
