using Aurora.Content.Items.Materials;

namespace Aurora.Content.Items.Armor.WildWarrior;

[AutoloadEquip(EquipType.Body)]
public class WildWarriorGarb : ModItem
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
            .AddIngredient<WildlifeFragment>(2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
