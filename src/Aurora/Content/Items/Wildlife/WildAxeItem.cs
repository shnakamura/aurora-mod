namespace Aurora.Content.Items.Wildlife;

public class WildAxeItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 4;
        Item.knockBack = 3f;

        Item.width = 36;
        Item.height = 32;

        Item.axe = 8;

        Item.useTime = 20;
        Item.useAnimation = 28;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>()
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
