using Aurora.Common.Projectiles.Components;

namespace Aurora.Common.Projectiles.Behavior;

/// <summary>
///     Provides a global that handles abilities and constraints of a projectile.
/// </summary>
/// <remarks>
///     This is widely used to determine whether other components can execute their behavior or not, preventing
///     race conditions.
/// </remarks>
public sealed class Abilities : GlobalProjectile
{
    /// <summary>
    ///     Whether the projectile attached to this component can have its velocity modified or not.
    /// </summary>
    public bool AbleToUpdateVelocity { get; private set; }
    
    public override void AI(Projectile projectile) {
        base.AI(projectile);

        AbleToUpdateVelocity = !projectile.TryGetGlobalProjectile(out Sticky sticky) || !sticky.IsStickingToAnything;
    }
}
