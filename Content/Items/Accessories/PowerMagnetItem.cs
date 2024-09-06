using ReLogic.Content;
using Terraria.DataStructures;

namespace Aurora.Content.Items.Accessories;

public class PowerMagnetItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.accessory = true;

		Item.width = 42;
		Item.height = 34;

		Item.value = Item.sellPrice(gold: 1);
	}
}
