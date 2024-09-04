using Aurora.Content.Projectiles.Ranged;

namespace Aurora.Content.Items.Weapons.Ranged.Tomahawks;

public class PlatinumTomahawkItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 17;
        Item.knockBack = 3f;

        Item.width = 28;
        Item.height = 24;

        Item.useTime = 17;
        Item.useAnimation = 17;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 13f;
        Item.shoot = ModContent.ProjectileType<PlatinumTomahawkProjectile>();
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe(25)
            .AddIngredient(ItemID.PlatinumBar)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
