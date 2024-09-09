using Aurora.Common.Behavior;
using Aurora.Core.Projectiles;

namespace Aurora.Content.Projectiles.Melee.Daggers;

public class GoldDaggerProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.width = 24;
		Projectile.height = 24;

		Projectile.TryEnableComponent<ProjectileDagger>();
	}
}
