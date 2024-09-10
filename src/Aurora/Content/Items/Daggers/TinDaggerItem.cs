using Aurora.Content.Projectiles.Daggers;

namespace Aurora.Content.Items.Daggers;

public class TinDaggerItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.maxStack = Item.CommonMaxStack;

		Item.noUseGraphic = true;
		Item.consumable = true;
		Item.autoReuse = false;
		Item.noMelee = true;

		Item.DamageType = DamageClass.Melee;
		Item.knockBack = 1f;
		Item.damage = 4;

		Item.width = 24;
		Item.height = 24;

		Item.useTime = 11;
		Item.useAnimation = 11;
		Item.UseSound = SoundID.Item1;
		Item.useStyle = ItemUseStyleID.Rapier;

		Item.rare = ItemRarityID.White;

		Item.shootSpeed = 2f;
		Item.shoot = ModContent.ProjectileType<TinDaggerProjectile>();
	}

	public override void AddRecipes() {
		base.AddRecipes();

		CreateRecipe(25)
			.AddIngredient(ItemID.TinBar)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
