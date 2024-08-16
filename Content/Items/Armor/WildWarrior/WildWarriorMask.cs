using Aurora.Content.Items.Materials;

namespace Aurora.Content.Items.Armor.WildWarrior;

[AutoloadEquip(EquipType.Head)]
public class WildWarriorMask : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 24;
        Item.height = 22;
        
        Item.defense = 1;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragment>(2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
