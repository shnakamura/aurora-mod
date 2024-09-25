using Aurora.Common.Behavior;
using Aurora.Core.Projectiles;

namespace Aurora.Content.Projectiles.Tomahawks;

public class LeadTomahawkProjectile : ModProjectile
{
    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.TryEnable<ProjectileTomahawk>();
    }
}
