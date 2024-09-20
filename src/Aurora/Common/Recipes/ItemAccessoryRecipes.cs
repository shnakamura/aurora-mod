using Aurora.Core.Configuration;

namespace Aurora.Common.Recipes;

/// <summary>
///		Handles the registration of recipes for uncraftable vanilla accessories.
/// </summary>
public sealed class ItemAccessoryRecipes : GlobalItem
{
	public override void AddRecipes() {
		base.AddRecipes();

		if (!ServerConfiguration.Instance.EnableRecipes) {
			return;
		}

		Recipe.Create(ItemID.PortableStool)
			.AddRecipeGroup(RecipeGroupID.Wood, 20)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(ItemID.Aglet)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(ItemID.Radar)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(ItemID.CloudinaBottle)
			.AddIngredient(ItemID.Glass, 5)
			.AddIngredient(ItemID.Cloud)
			.AddTile(TileID.WorkBenches)
			.Register();

		Recipe.Create(ItemID.HermesBoots)
			.AddIngredient(ItemID.Silk, 5)
			.AddTile(TileID.WorkBenches)
			.Register();

		Recipe.Create(ItemID.AnkletoftheWind)
			.AddIngredient(ItemID.JungleSpores, 10)
			.AddTile(TileID.WorkBenches)
			.Register();

		Recipe.Create(ItemID.ShoeSpikes)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(ItemID.ClimbingClaws)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
