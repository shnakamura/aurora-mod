// This file is placed outside of the common 'Utilities/Extensions' scope for the sake of convenience when using components.
namespace Aurora.Common.Projectiles.Components;

/// <summary>
///     Provides basic extensions that interface with <see cref="ProjectileComponent"/>.
/// </summary>
public static class ProjectileComponentExtensions
{
    /// <summary>
    ///     Attempts to enable a specified component on a projectile.
    /// </summary>
    /// <param name="projectile">The projectile on which the component is to be enabled.</param>
    /// <param name="initializer">An optional delegate to initialize the component after it has been enabled.</param>
    /// <typeparam name="T">The type of the component to enable, which must inherit from <see cref="ProjectileComponent"/>.</typeparam>
    /// <returns><c>true</c> if the component was successfully enabled; otherwise, <c>false</c>.</returns>
    public static bool TryEnableComponent<T>(this Projectile projectile, Action<T>? initializer = null) where T : ProjectileComponent {
        var hasComponent = projectile.TryGetGlobalProjectile(out T? component);
    
        if (!hasComponent) {
            return false;
        }

        component.Enabled = true;
    
        initializer?.Invoke(component);

        return true;
    }
    
    /// <summary>
    ///     Attempts to disable a specified component on a projectile.
    /// </summary>
    /// <param name="projectile">The projectile on which the component is to be disabled</param>
    /// <typeparam name="T">The type of the component to enable, which must inherit from <see cref="ProjectileComponent"/>.</typeparam>
    /// <returns><c>true</c> if the component was successfully disabled; otherwise, <c>false</c>.</returns>
    public static bool TryDisableComponent<T>(this Projectile projectile) where T : ProjectileComponent {
        var hasComponent = projectile.TryGetGlobalProjectile(out T? component);
    
        if (!hasComponent) {
            return false;
        }

        component.Enabled = false;

        return true;
    }
}
