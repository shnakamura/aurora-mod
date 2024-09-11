using Aurora.Content.Items.Forest;

namespace Aurora.Content.Items.Wildlife;

public class WildlifeFragmentItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.width = 30;
        Item.height = 26;

        Item.value = Item.sellPrice(copper: 10);
                
        Item.rare = ItemRarityID.Blue;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar)
            .AddIngredient(RecipeGroupID.Wood, 4)
            .AddIngredient<AncientTwigItem>(2)
            .AddIngredient<NutItem>()
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
