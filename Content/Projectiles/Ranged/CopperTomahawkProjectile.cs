using Aurora.Utilities;

namespace Aurora.Content.Projectiles.Ranged;

public class CopperTomahawkProjectile : ModProjectile
{
    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.tileCollide = true;
        Projectile.friendly = true;

        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
        
        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.penetrate = -1;
    }
}
