using Aurora.Common.Projectiles;
using Aurora.Core.Projectiles;

namespace Aurora.Content.Projectiles.Melee.Daggers;

public class IronDaggerProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.width = 20;
		Projectile.height = 20;

		Projectile.TryEnableComponent<ProjectileDagger>();
	}
}
