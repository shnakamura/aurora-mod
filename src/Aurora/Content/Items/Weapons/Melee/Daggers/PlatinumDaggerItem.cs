using Aurora.Content.Projectiles.Melee.Daggers;

namespace Aurora.Content.Items.Weapons.Melee.Daggers;

public class PlatinumDaggerItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.maxStack = Item.CommonMaxStack;

		Item.noUseGraphic = true;
		Item.consumable = true;
		Item.autoReuse = false;
		Item.noMelee = true;

		Item.DamageType = DamageClass.Melee;
		Item.knockBack = 2.5f;
		Item.damage = 12;

		Item.width = 24;
		Item.height = 24;

		Item.useTime = 9;
		Item.useAnimation = 9;
		Item.UseSound = SoundID.Item1;
		Item.useStyle = ItemUseStyleID.Rapier;

		Item.rare = ItemRarityID.White;

		Item.shootSpeed = 3.5f;
		Item.shoot = ModContent.ProjectileType<PlatinumDaggerProjectile>();
	}

	public override void AddRecipes() {
		base.AddRecipes();

		CreateRecipe(25)
			.AddIngredient(ItemID.PlatinumBar)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
