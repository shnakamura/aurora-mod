namespace Aurora.Content.Items.Wildlife;

public class WildPickaxeItem : ModItem
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
