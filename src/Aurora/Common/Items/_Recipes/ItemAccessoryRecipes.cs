using Aurora.Core.Configuration;

namespace Aurora.Common.Items;

public sealed class ItemAccessoryRecipes : GlobalItem
{
	public override void AddRecipes() {
		base.AddRecipes();

		if (!ServerConfiguration.Instance.EnableRecipes) {
			return;
		}

		Recipe.Create(ItemID.Aglet)
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
		
		Recipe.Create(ItemID.MagicMirror)
			.AddIngredient(ItemID.Glass, 20)
			.AddIngredient(ItemID.FallenStar, 3)
			.AddTile(TileID.WorkBenches)
			.Register();
		
		Recipe.Create(ItemID.IceMirror)
			.AddIngredient(ItemID.Glass, 20)
			.AddIngredient(ItemID.FallenStar, 3)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
