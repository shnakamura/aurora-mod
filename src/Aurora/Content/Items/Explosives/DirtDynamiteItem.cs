namespace Aurora.Content.Items.Explosives;

public class DirtDynamiteItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();
		
		Item.CloneDefaults(ItemID.Dynamite);
		
		Item.width = 18;
		Item.height = 38;
	}

	public override void AddRecipes() {
		base.AddRecipes();

		CreateRecipe()
			.AddIngredient(ItemID.Dynamite)
			.AddIngredient(ItemID.DirtBlock, 50)
			.Register();
	}
}
