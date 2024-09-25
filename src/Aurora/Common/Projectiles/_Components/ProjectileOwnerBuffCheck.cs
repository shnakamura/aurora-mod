using Aurora.Core.Projectiles;

namespace Aurora.Common.Projectiles;

/// <summary>
///		Handles the behavior of projectiles that are conditionally alive from the player being alive
///		and having a specific buff active.
/// </summary>
/// <remarks>
///		This type of behavior is widely utilized by projectiles that are minions.
/// </remarks>
public sealed class ProjectileOwnerBuffCheck : ProjectileComponent
{
	public struct CheckData(int type)
	{
		/// <summary>
		///		The type of buff to check.
		/// </summary>
		public int Type = type;
	}

	public CheckData Data;

	public override void AI(Projectile projectile) {
		base.AI(projectile);

		if (!Enabled) {
			return;
		}

		var owner = Main.player[projectile.owner];

		if (!owner.active || owner.dead || owner.ghost) {
			owner.ClearBuff(Data.Type);
		}

		if (owner.HasBuff(Data.Type)) {
			projectile.timeLeft = 2;
		}
	}
}
