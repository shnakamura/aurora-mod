using Aurora.Core.Configuration;

namespace Aurora.Common.Recipes;

public sealed class ItemWeaponRecipes : GlobalItem
{
	public override void AddRecipes() {
		base.AddRecipes();

		if (!ServerConfiguration.Instance.EnableRecipes) {
			return;
		}
		
		Recipe.Create(ItemID.Shuriken, 25)
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddTile(TileID.Anvils)
			.Register();
		
		Recipe.Create(ItemID.ThrowingKnife, 25)
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddTile(TileID.Anvils)
			.Register();
		
		Recipe.Create(ItemID.WandofSparking)
			.AddRecipeGroup(RecipeGroupID.Wood, 10)
			.AddIngredient(ItemID.FallenStar, 1)
			.AddTile(TileID.WorkBenches)
			.Register();
		
		Recipe.Create(ItemID.WoodenBoomerang)
			.AddRecipeGroup(RecipeGroupID.Wood, 10)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
