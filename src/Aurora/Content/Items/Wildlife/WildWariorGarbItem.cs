namespace Aurora.Content.Items.Wildlife;

[AutoloadEquip(EquipType.Body)]
public class WildWarriorGarbItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 38;
        Item.height = 22;

        Item.defense = 2;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>(2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
