using Aurora.Content.Items.Materials;

namespace Aurora.Content.Items.Armor.WildWarrior;

[AutoloadEquip(EquipType.Head)]
public class WildWarriorHelmet : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 22;
        Item.height = 16;
        
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
