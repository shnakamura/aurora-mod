namespace Aurora.Common.Items;

public sealed class ItemConsumableRecipes : GlobalItem
{
	public override void AddRecipes() {
		base.AddRecipes();
		
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
