namespace Aurora.Content.Items.Everbloom;

public class BloomRose : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.maxStack = Item.CommonMaxStack;

		Item.width = 26;
		Item.height = 26;

		Item.rare = ItemRarityID.Blue;
	}
}
