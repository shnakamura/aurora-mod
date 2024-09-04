using Aurora.Content.Projectiles.Ranged;

namespace Aurora.Content.Items.Weapons.Ranged.Tomahawks;

public class LeadTomahawkItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 8;
        Item.knockBack = 3f;

        Item.width = 28;
        Item.height = 24;

        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 9f;
        Item.shoot = ModContent.ProjectileType<LeadTomahawkProjectile>();
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe(25)
            .AddIngredient(ItemID.LeadBar)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
