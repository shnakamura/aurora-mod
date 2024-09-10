namespace Aurora.Content.Items.Explosives;

public class StickyDirtDynamiteItem : ModItem
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
			.AddIngredient(ItemID.StickyDynamite)
			.AddIngredient(ItemID.DirtBlock, 50)
			.Register();

		CreateRecipe()
			.AddIngredient<DirtDynamiteItem>()
			.AddIngredient(ItemID.Gel)
			.Register();
	}
}
