namespace Aurora.Content.Projectiles.Daggers;

public class GoldDaggerProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.width = 24;
		Projectile.height = 24;
	}
}
