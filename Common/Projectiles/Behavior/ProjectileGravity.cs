using Aurora.Common.Projectiles.Components;

namespace Aurora.Common.Projectiles.Behavior;

public sealed class ProjectileGravity : ProjectileComponent
{
    public struct GravityData
    {
        /// <summary>
        ///     The value that will be incremented to the projectile's vertical velocity every tick.
        /// </summary>
        /// <remarks>
        ///     Defaults to <c>0.2f</c>.
        /// </remarks>
        public float Value = 0.2f;

        public GravityData() { }
    }

    /// <summary>
    ///     The gravity data parameters associated with the projectile.
    /// </summary>
    public GravityData Data = new();
    
    public override void AI(Projectile projectile) {
        base.AI(projectile);

        if (!Enabled) {
            return;
        }

        var isSticking = projectile.TryGetGlobalProjectile(out ProjectileSticky sticky) && sticky.IsStickingToAnything;

        if (isSticking) {
            return;
        }

        projectile.velocity.Y += Data.Value;
    }
}
