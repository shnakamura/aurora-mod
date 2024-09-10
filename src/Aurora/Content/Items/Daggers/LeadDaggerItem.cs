using Aurora.Content.Projectiles.Daggers;

namespace Aurora.Content.Items.Daggers;

public class LeadDaggerItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.maxStack = Item.CommonMaxStack;

		Item.noUseGraphic = true;
		Item.consumable = true;
		Item.autoReuse = false;
		Item.noMelee = true;

		Item.DamageType = DamageClass.Melee;
		Item.knockBack = 1.5f;
		Item.damage = 6;

		Item.width = 24;
		Item.height = 24;

		Item.useTime = 12;
		Item.useAnimation = 12;
		Item.UseSound = SoundID.Item1;
		Item.useStyle = ItemUseStyleID.Rapier;

		Item.rare = ItemRarityID.White;

		Item.shootSpeed = 2.4f;
		Item.shoot = ModContent.ProjectileType<LeadDaggerProjectile>();
	}

	public override void AddRecipes() {
		base.AddRecipes();

		CreateRecipe(25)
			.AddIngredient(ItemID.LeadBar)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
