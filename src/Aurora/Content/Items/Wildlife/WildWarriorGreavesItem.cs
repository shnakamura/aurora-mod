namespace Aurora.Content.Items.Wildlife;

[AutoloadEquip(EquipType.Legs)]
public class WildWarriorGreavesItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 22;
        Item.height = 16;

        Item.defense = 1;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>()
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
