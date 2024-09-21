using Terraria.GameContent;
using Terraria.GameContent.Tile_Entities;

namespace Aurora.Content.Items.Miscellaneous;

public class PocketCatalystItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.width = 40;
		Item.height = 24;

		Item.value = Item.sellPrice(platinum: 1);

		Item.rare = ItemRarityID.Blue;
	}
}
