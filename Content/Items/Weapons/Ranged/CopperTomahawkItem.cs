namespace Aurora.Content.Items.Weapons.Ranged;

public class CopperTomahawkItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        Item.autoReuse = true;
        
        Item.DamageType = DamageClass.Melee;
        Item.damage = 4;
        Item.knockBack = 2f;
        
        Item.width = 28;
        Item.height = 24;

        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 10f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.CopperTomahawkProjectile>();
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe(25)
            .AddIngredient(ItemID.CopperBar)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
