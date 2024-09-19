using Terraria.DataStructures;

namespace Aurora.Content.Items.Scarlet;

public sealed class ScarletPlayerDrawLayer : PlayerDrawLayer
{
	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
		return true;
	}

	public override Position GetDefaultPosition() {
		return new AfterParent(PlayerDrawLayers.Head);
	}

	protected override void Draw(ref PlayerDrawSet drawInfo) {

	}
}
