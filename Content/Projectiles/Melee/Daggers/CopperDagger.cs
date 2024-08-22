using Terraria.Enums;

namespace Aurora.Content.Projectiles.Melee.Daggers;

public class CopperDagger : ProjectileDaggerBase
{
    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.width = 16;
        Projectile.height = 16;
    }
}

