using Aurora.Content.Projectiles.Tomahawks;

namespace Aurora.Content.Items.Tomahawks;

public class GoldTomahawkItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.knockBack = 3f;
        Item.damage = 15;

        Item.width = 28;
        Item.height = 24;

        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 13f;
        Item.shoot = ModContent.ProjectileType<GoldTomahawkProjectile>();
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe(25)
            .AddIngredient(ItemID.GoldBar)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
