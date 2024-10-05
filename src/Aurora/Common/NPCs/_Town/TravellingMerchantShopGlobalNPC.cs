using Aurora.Content.Items.Miscellaneous;

namespace Aurora.Common.NPCs;

public sealed class TravellingMerchantShopGlobalNPC : GlobalNPC
{
	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
		return entity.type == NPCID.TravellingMerchant;
	}

	public override void SetupTravelShop(int[] shop, ref int nextSlot) {
		base.SetupTravelShop(shop, ref nextSlot);

		if (Main.rand.NextBool(10)) {
			shop[nextSlot++] = ModContent.ItemType<PocketCatalystItem>();
		}

		if (Main.rand.NextBool(5)) {
			shop[nextSlot++] = ModContent.ItemType<PowerMagnetItem>();
		}
	}
}
