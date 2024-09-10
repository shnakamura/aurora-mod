using Aurora.Content.Projectiles.Ranged;

namespace Aurora.Content.Items.Tomahawks;

public class IronTomahawkItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.knockBack = 2.5f;
        Item.damage = 6;

        Item.width = 28;
        Item.height = 24;

        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 11f;
        Item.shoot = ModContent.ProjectileType<IronTomahawkProjectile>();
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe(25)
            .AddIngredient(ItemID.IronBar)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
