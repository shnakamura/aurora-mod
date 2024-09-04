using Aurora.Common.Projectiles;
using Aurora.Core.Projectiles;

namespace Aurora.Content.Projectiles.Melee.Daggers;

public class SilverDaggerProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.width = 22;
		Projectile.height = 22;

		Projectile.TryEnableComponent<ProjectileDagger>();
	}
}
