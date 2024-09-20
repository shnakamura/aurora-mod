namespace Aurora.Content.Items.Miscellaneous;

[AutoloadEquip(EquipType.Shoes)]
public class StoneBootsItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.accessory = true;

		Item.width = 32;
		Item.height = 28;
	}

	public override void UpdateAccessory(Player player, bool hideVisual) {
		base.UpdateAccessory(player, hideVisual);

		if (!player.TryGetModPlayer(out StoneBootsPlayer modPlayer) || !player.controlDown) {
			return;
		}

		modPlayer.Enabled = true;
	}

	public override void AddRecipes() {
		base.AddRecipes();

		CreateRecipe()
			.AddIngredient(ItemID.StoneBlock, 20)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
