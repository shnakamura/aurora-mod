using System.Collections.Generic;
using StructureHelper;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Aurora.Common.World;

public sealed class IceRuinsMicroBiome : MicroBiome
{
	public override bool Place(Point origin, StructureMap structures) {
		var mod = Aurora.Instance;

		return true;
	}
}
