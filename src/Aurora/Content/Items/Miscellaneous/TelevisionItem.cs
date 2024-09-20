using Aurora.Content.Tiles;

namespace Aurora.Content.Items.Miscellaneous;

public class TelevisionItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.DefaultToPlaceableTile(ModContent.TileType<TelevisionTile>());
	}
}
