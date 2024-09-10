using Aurora.Content.Projectiles.Daggers;

namespace Aurora.Content.Items.Daggers;

public class GoldDaggerItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.maxStack = Item.CommonMaxStack;

		Item.noUseGraphic = true;
		Item.consumable = true;
		Item.autoReuse = false;
		Item.noMelee = true;

		Item.DamageType = DamageClass.Melee;
		Item.knockBack = 2.2f;
		Item.damage = 10;

		Item.width = 24;
		Item.height = 24;

		Item.useTime = 10;
		Item.useAnimation = 10;
		Item.UseSound = SoundID.Item1;
		Item.useStyle = ItemUseStyleID.Rapier;

		Item.rare = ItemRarityID.White;

		Item.shootSpeed = 3f;
		Item.shoot = ModContent.ProjectileType<GoldDaggerProjectile>();
	}

	public override void AddRecipes() {
		base.AddRecipes();

		CreateRecipe(25)
			.AddIngredient(ItemID.GoldBar)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
