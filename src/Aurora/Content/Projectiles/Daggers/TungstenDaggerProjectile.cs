using Aurora.Common.Behavior;
using Aurora.Core.Projectiles;

namespace Aurora.Content.Projectiles.Daggers;

public class TungstenDaggerProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.width = 22;
		Projectile.height = 22;

		Projectile.TryEnable<ProjectileDagger>();
	}
}
