using Aurora.Common.Projectiles.Behavior;

namespace Aurora.Utilities;

public static class ProjectileExtensions
{
    public static bool AbleToUpdateVelocity(this Projectile projectile) {
        return !projectile.TryGetGlobalProjectile(out Abilities component) || component.AbleToUpdateVelocity;
    }
}
