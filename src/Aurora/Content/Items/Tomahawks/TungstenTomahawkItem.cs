using Aurora.Content.Projectiles.Ranged;

namespace Aurora.Content.Items.Tomahawks;

public class TungstenTomahawkItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.knockBack = 4f;
        Item.damage = 12;

        Item.width = 28;
        Item.height = 24;

        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 10f;
        Item.shoot = ModContent.ProjectileType<TungstenTomahawkProjectile>();
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe(25)
            .AddIngredient(ItemID.TungstenBar)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
