namespace Aurora.Content.Items.Consumables;

public class DirtDynamiteItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.maxStack = Item.CommonMaxStack;

		Item.consumable = true;
		
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
