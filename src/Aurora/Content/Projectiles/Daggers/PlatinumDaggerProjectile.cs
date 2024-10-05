namespace Aurora.Content.Projectiles.Daggers;

public class PlatinumDaggerProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.width = 24;
		Projectile.height = 24;
	}
}
