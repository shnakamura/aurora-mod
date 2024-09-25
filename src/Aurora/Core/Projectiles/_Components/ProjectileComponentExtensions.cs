// This file is placed outside of the common 'Utilities/Extensions' scope for the sake of convenience when using components.

using System.Diagnostics.CodeAnalysis;

namespace Aurora.Core.Projectiles;

/// <summary>
///     Provides <see cref="Projectile"/> extensions that interface with <see cref="ProjectileComponent" />.
/// </summary>
public static class ProjectileComponentExtensions
{
	public static bool TryEnable<T>(this Projectile projectile, Action<T>? initializer = null) where T : ProjectileComponent {
		if (!projectile.TryGet<T>(out var component)) {
			return false;
		}

		component.Enabled = true;

		initializer?.Invoke(component);

		return true;
	}

	public static bool TryGet<T>(this Projectile projectile, [MaybeNullWhen(false)] out T? component) where T : ProjectileComponent {
		return projectile.TryGetGlobalProjectile(out component) && component.Enabled;
	}
}
