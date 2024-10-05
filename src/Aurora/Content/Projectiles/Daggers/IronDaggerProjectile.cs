namespace Aurora.Content.Projectiles.Daggers;

public class IronDaggerProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.width = 20;
		Projectile.height = 20;
	}
}
