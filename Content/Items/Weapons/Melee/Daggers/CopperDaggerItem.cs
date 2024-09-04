using Terraria.Enums;

namespace Aurora.Content.Items.Weapons.Melee.Daggers;

public class CopperDaggerItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;
        
        Item.noUseGraphic = true;
        Item.autoReuse = false;
        Item.noMelee = true;
        
        Item.DamageType = DamageClass.Melee;
        Item.damage = 5;
        Item.knockBack = 1f;
        
        Item.width = 24;
        Item.height = 24;
        
        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Rapier;
        
        Item.rare = ItemRarityID.White;

        Item.shootSpeed = 2f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Daggers.CopperDaggerProjectile>(); 
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe(25)
            .AddIngredient(ItemID.CopperBar)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
