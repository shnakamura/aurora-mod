namespace Aurora.Content.Items.Weapons.Ranged;

public class BigIronItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.DamageType = DamageClass.Ranged;
        Item.damage = 16;
        Item.knockBack = 2f;

        Item.width = 42;
        Item.height = 22;

        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.UseSound = SoundID.Item11;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Bullet;
        Item.shootSpeed = 8f;
        Item.shoot = ProjectileID.PurificationPowder;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 8)
            .AddRecipeGroup(RecipeGroupID.Wood, 2)
            .AddTile(TileID.Anvils)
            .Register();
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

        velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5f));
    }
    
    public override Vector2? HoldoutOffset() {
        return new Vector2(2f, -2f);
    }
}
