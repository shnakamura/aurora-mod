using Aurora.Content.Items.Materials;

namespace Aurora.Content.Items.Weapons.Melee;

public class WildNatureItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;
        Item.useTurn = true;

        Item.DamageType = DamageClass.Melee;
        Item.knockBack = 2f;
        Item.damage = 11;

        Item.width = 44;
        Item.height = 52;

        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>(2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
