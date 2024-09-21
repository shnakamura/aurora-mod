namespace Aurora.Content.Items.Miscellaneous;

public class PowerMagnetItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.accessory = true;

		Item.width = 42;
		Item.height = 34;

		Item.value = Item.sellPrice(gold: 1);

		Item.rare = ItemRarityID.Blue;
	}
}
