using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Aurora.Common.Materials;
using Aurora.Core.Configuration;
using Aurora.Utilities;
using JetBrains.Annotations;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class FootstepsSystem : ModSystem
{
	private static Dictionary<string, IFootstep>? footstepsByMaterial = [];
	
	public override void Load() {
		base.Load();

		foreach (var footstep in ModContent.GetContent<IFootstep>()) {
			footstepsByMaterial[footstep.Material] = footstep;
		}
	}

	public override void Unload() {
		base.Unload();
		
		footstepsByMaterial?.Clear();
		footstepsByMaterial = null;
	}

	public static bool TryGetFootstep(int tileType, [MaybeNullWhen(false)] out IFootstep? footstep) {
		footstep = null;
		
		if (!TileMaterialSystem.TryGetMaterial(tileType, out var materialName)) {
			return false;
		}

		return footstepsByMaterial.TryGetValue(materialName, out footstep);
	}
}
