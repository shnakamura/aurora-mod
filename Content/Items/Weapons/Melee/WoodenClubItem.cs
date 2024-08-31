namespace Aurora.Content.Items.Weapons.Melee;

public class WoodenClubItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;
        Item.useTurn = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 12;
        Item.knockBack = 4f;

        Item.width = 32;
        Item.height = 34;

        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient(ItemID.Wood, 15)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
