using Aurora.Content.Projectiles.Ranged;

namespace Aurora.Content.Items.Weapons.Ranged.Tomahawks;

public class GoldTomahawkItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 15;
        Item.knockBack = 3f;

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
