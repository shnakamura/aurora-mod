using Aurora.Content.Tiles;

namespace Aurora.Assets.Textures.Items.Miscellaneous;

public class TelevisionItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();
		
		Item.DefaultToPlaceableTile(ModContent.TileType<TelevisionTile>());
	}
}
