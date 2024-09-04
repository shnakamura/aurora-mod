using Aurora.Common.Projectiles;
using Aurora.Core.Projectiles;
using Aurora.Utilities;

namespace Aurora.Content.Projectiles.Ranged;

public class CopperTomahawkProjectile : ModProjectile
{
    public override void SetDefaults() {
        base.SetDefaults();
        
        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.TryEnableComponent<ProjectileTomahawk>();
    }
}
