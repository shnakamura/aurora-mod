using Aurora.Content.Projectiles.Ranged;

namespace Aurora.Content.Items.Tomahawks;

public class TinTomahawkItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.knockBack = 2f;
        Item.damage = 3;

        Item.width = 28;
        Item.height = 24;

        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 11f;
        Item.shoot = ModContent.ProjectileType<TinTomahawkProjectile>();
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe(25)
            .AddIngredient(ItemID.TinBar)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
