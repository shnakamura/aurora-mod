using Aurora.Common.Behavior;
using Aurora.Core.Projectiles;

namespace Aurora.Content.Projectiles.Ranged;

public class TinTomahawkProjectile : ModProjectile
{
    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.TryEnableComponent<ProjectileTomahawk>();
    }
}
