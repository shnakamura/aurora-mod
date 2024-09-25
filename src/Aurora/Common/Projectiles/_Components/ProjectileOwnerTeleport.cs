using Aurora.Core.Projectiles;

namespace Aurora.Common.Projectiles;

/// <summary>
///		Handles the behavior of projectiles that should teleport back to their owner if they're too
///		far away.
/// </summary>
/// <remarks>
///		This type of behavior is widely utilized by projectiles that are minions.
/// </remarks>
public sealed class ProjectileOwnerTeleport : ProjectileComponent
{
	public struct TeleportData(float squaredTeleportRange)
	{
		/// <summary>
		///		The squared teleport range of the projectile in pixels.
		/// </summary>
		/// <remarks>
		///		The projectile will be teleported to its owner if it's farther away from this range.
		/// </remarks>
		public float SquaredTeleportRange = squaredTeleportRange;
	}

	public TeleportData Data;

	public override void AI(Projectile projectile) {
		base.AI(projectile);

		if (!Enabled) {
			return;
		}

		var owner = Main.player[projectile.owner];

		if (projectile.DistanceSQ(owner.Center) <= Data.SquaredTeleportRange * Data.SquaredTeleportRange) {
			return;
		}

		projectile.Center = owner.Center;
	}
}
