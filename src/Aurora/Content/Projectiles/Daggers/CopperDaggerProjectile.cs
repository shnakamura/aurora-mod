using Aurora.Common.Behavior;
using Aurora.Core.Projectiles;

namespace Aurora.Content.Projectiles.Melee.Daggers;

public class CopperDaggerProjectile : ModProjectile
{
    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.width = 20;
        Projectile.height = 20;

        Projectile.TryEnableComponent<ProjectileDagger>();
    }
}
