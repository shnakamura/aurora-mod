using Aurora.Content.Items.Materials;

namespace Aurora.Content.Items.Tools;

public class WildPickaxe : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 3;
        Item.knockBack = 1.5f;
        
        Item.width = 34;
        Item.height = 30;

        Item.pick = 40;

        Item.useTime = 15;
        Item.useAnimation = 22;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragment>()
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
