using Aurora.Content.Items.Materials;

namespace Aurora.Content.Items.Weapons.Melee;

public class BearClawsItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;
        Item.useTurn = true;

        Item.DamageType = DamageClass.Melee;
        Item.knockBack = 1.5f;
        Item.damage = 4;

        Item.width = 32;
        Item.height = 28;

        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient(ItemID.Wood, 20)
            .AddIngredient<AncientTwigItem>(4)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
