using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Aurora.Common.World;

public sealed class IceRuinsSystem : ModSystem
{
	public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
		base.ModifyWorldGenTasks(tasks, ref totalWeight);

		var index = tasks.FindIndex(static pass => pass.Name == "");

		if (index == -1) {
			return;
		}
		
		tasks.Insert(index + 1, new PassLegacy($"{nameof(Aurora)}:{nameof(IceRuinsMicroBiome)}", GenerateIceRuins));
	}

	private static void GenerateIceRuins(GenerationProgress progress, GameConfiguration configuration) {
		var biome = GenVars.configuration.CreateBiome<IceRuinsMicroBiome>();
	}
}
