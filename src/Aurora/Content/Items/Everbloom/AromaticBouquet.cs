using Aurora.Content.Buffs;
using Aurora.Content.Projectiles.Everbloom;

namespace Aurora.Content.Items.Everbloom;

public class AromaticBouquet : ModItem
{
	public override void SetStaticDefaults() {
		base.SetStaticDefaults();

		ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
		ItemID.Sets.LockOnIgnoresCollision[Type] = true;

		ItemID.Sets.StaffMinionSlotsRequired[Type] = 0.5f;
	}

	public override void SetDefaults() {
		base.SetDefaults();

		Item.DamageType = DamageClass.Summon;

		Item.noMelee = true;

		Item.damage = 8;
		Item.knockBack = 1f;
		Item.mana = 10;

		Item.width = 28;
		Item.height = 24;

		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.UseSound = SoundID.Item44;
		Item.useStyle = ItemUseStyleID.Swing;

		Item.buffType = ModContent.BuffType<BloomBulbBuff>();

		Item.shoot = ModContent.ProjectileType<BloomBulbProjectile>();
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

		position = Main.MouseWorld;
	}

	public override void AddRecipes() {
		base.AddRecipes();

		CreateRecipe()
			.AddIngredient<BloomRose>(2)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
