namespace Aurora.Content.Items.Explosives;

public class StickyDirtDynamiteItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();
		
		Item.CloneDefaults(ItemID.StickyDynamite);
		
		Item.width = 18;
		Item.height = 38;
	}

	public override void AddRecipes() {
		base.AddRecipes();

		CreateRecipe()
			.AddIngredient(ItemID.Dynamite)
			.AddIngredient(ItemID.DirtBlock, 50)
			.AddIngredient(ItemID.Gel)
			.Register();

		CreateRecipe()
			.AddIngredient<DirtDynamiteItem>()
			.AddIngredient(ItemID.Gel)
			.Register();
	}
}
