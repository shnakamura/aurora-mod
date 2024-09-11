namespace Aurora.Content.Items.Wildlife;

public class BarkBallistaItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;
        Item.noMelee = true;

        Item.DamageType = DamageClass.Ranged;
        Item.knockBack = 3f;
        Item.damage = 18;

        Item.width = 48;
        Item.height = 24;

        Item.useTime = 28;
        Item.useAnimation = 28;
        Item.UseSound = SoundID.Item5;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.useAmmo = AmmoID.Arrow;
        Item.shootSpeed = 8f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
                
        Item.rare = ItemRarityID.Blue;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>(2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }

    public override void ModifyShootStats(
        Player player,
        ref Vector2 position,
        ref Vector2 velocity,
        ref int type,
        ref int damage,
        ref float knockback
    ) {
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

        velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5f));
    }

    public override Vector2? HoldoutOffset() {
        return new Vector2(2f, -2f);
    }
}
