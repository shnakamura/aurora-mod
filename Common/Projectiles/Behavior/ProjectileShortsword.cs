using Aurora.Common.Projectiles.Components;

namespace Aurora.Common.Projectiles.Behavior;

public sealed class ProjectileShortsword : ProjectileComponent
{
    public override void AI(Projectile projectile) {
        base.AI(projectile);
        
        if (!Enabled) {
            return;
        }

        var isSticking = projectile.TryGetGlobalProjectile(out ProjectileSticky sticky) && sticky.IsStickingToAnything;

        if (isSticking) {
            return;
        }

        
    }
}
