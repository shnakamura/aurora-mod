using Terraria.Enums;

namespace Aurora.Content.Projectiles.Melee.Daggers;

public class CopperDaggerProjectile : DaggerProjectileBase
{
    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.width = 16;
        Projectile.height = 16;
    }
}

