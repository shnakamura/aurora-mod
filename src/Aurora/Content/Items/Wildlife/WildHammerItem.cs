namespace Aurora.Content.Items.Wildlife;

public class WildHammerItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 6;
        Item.knockBack = 4f;

        Item.width = 34;
        Item.height = 34;

        Item.hammer = 35;

        Item.useTime = 22;
        Item.useAnimation = 30;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
                
        Item.rare = ItemRarityID.Blue;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>()
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
