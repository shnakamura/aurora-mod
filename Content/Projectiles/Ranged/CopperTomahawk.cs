using Aurora.Common.Projectiles.Behavior;
using Aurora.Common.Projectiles.Components;
using Aurora.Utilities;

namespace Aurora.Content.Projectiles.Ranged;

public class CopperTomahawk : ModProjectile
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

        Projectile.TryEnableComponent<Gravity>();
        Projectile.TryEnableComponent<HorizontalRotation>();
        Projectile.TryEnableComponent<Sticky>(static c => { c.Data.Flags = StickyFlags.All; });
    }
}
