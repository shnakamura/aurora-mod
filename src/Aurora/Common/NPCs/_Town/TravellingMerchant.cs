using Aurora.Content.Items.Miscellaneous;

namespace Aurora.Common.NPCs;

public sealed class TravellingMerchant : GlobalNPC
{
	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
		return entity.type == NPCID.TravellingMerchant;
	}

	public override void SetupTravelShop(int[] shop, ref int nextSlot) {
		base.SetupTravelShop(shop, ref nextSlot);

		shop[nextSlot++] = ModContent.ItemType<PowerMagnetItem>();
	}
}
