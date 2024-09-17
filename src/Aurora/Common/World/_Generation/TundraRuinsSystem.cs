using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Aurora.Common.World;

public sealed class TundraRuinsSystem : ModSystem
{
	public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
		base.ModifyWorldGenTasks(tasks, ref totalWeight);

		var index = tasks.FindIndex(static pass => pass.Name == "Micro Biomes");

		if (index == -1) {
			return;
		}

		// tasks.Insert(index + 1, new PassLegacy($"{nameof(Aurora)}:{nameof(TundraRuinsMicroBiome)}", GenerateTundraRuins));
	}

	private static void GenerateTundraRuins(GenerationProgress progress, GameConfiguration configuration) {
		progress.Message = "Generating ruins...";

		var tundraStart = 0;
		var tundraEnd = 0;

		for (var i = Main.maxTilesX; i > 0; i--) {
			for (var j = 0; j < Main.maxTilesY; j++) {
				var tile = Framing.GetTileSafely(i, j);

				if (tile.HasTile && tile.TileType == TileID.IceBlock) {
					tundraStart = i;
					break;
				}
			}
		}

		for (var i = 0; i < Main.maxTilesX; i++) {
			for (var j = 0; j < Main.maxTilesY; j++) {
				var tile = Framing.GetTileSafely(i, j);

				if (tile.HasTile && tile.TileType == TileID.IceBlock) {
					tundraEnd = i;
					break;
				}
			}
		}

		var tundraBottom = 0;

		var tundraWidth = Math.Abs(tundraStart - tundraEnd);
		var tundraCenter = tundraStart + tundraWidth / 2;

		for (var i = Main.maxTilesY; i > 0; i--) {
			var tile = Framing.GetTileSafely(tundraCenter, i);

			if (tile.HasTile && tile.TileType == TileID.IceBlock) {
				tundraBottom = i;
				break;
			}
		}

		var biome = GenVars.configuration.CreateBiome<TundraRuinsMicroBiome>();
		var biomeGenerated = false;

		while (!biomeGenerated) {
			var x = WorldGen.genRand.Next(tundraStart + 50, tundraEnd - 50);

			WorldUtils.Find(new Point(x, 0),
				Searches.Chain(
					new Searches.Down((int)Main.worldSurface + 200),
					new Conditions.IsSolid(),
					new Conditions.IsTile(TileID.IceBlock, TileID.SnowBlock)
				),
				out var origin
			);

			if (biome.Place(origin, GenVars.structures)) {
				biomeGenerated = true;
				break;
			}
		}
	}
}
