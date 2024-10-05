namespace Aurora.Content.Projectiles.Tomahawks;

public class PlatinumTomahawkProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.width = 16;
		Projectile.height = 16;
	}
}
