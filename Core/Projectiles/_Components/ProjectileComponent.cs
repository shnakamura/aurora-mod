namespace Aurora.Core.Projectiles;

public abstract class ProjectileComponent : GlobalProjectile
{
    /// <summary>
    ///     Whether this component is enabled or not.
    /// </summary>
    public bool Enabled { get; set; }

    public sealed override bool InstancePerEntity { get; } = true;
}
