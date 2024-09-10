using Aurora.Content.Projectiles.Summon;

namespace Aurora.Content.Items.Beach;

public class CrabPincersItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.sentry = true;

        Item.DamageType = DamageClass.Summon;
        Item.knockBack = 2f;
        Item.damage = 20;
        Item.mana = 20;

        Item.width = 42;
        Item.height = 40;

        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<CrabPincersProjectile>();
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

        var tileCoordinates = Main.MouseWorld.ToTileCoordinates();

        position = Main.MouseWorld;

        for (var i = tileCoordinates.Y; i < Main.maxTilesY; i++) {
            var tile = Framing.GetTileSafely(tileCoordinates.X, i);

            if (WorldGen.SolidTile(tile)) {
                position = new Vector2(tileCoordinates.X, i - 1) * 16f;
                break;
            }
        }

        player.UpdateMaxTurrets();
    }
}
