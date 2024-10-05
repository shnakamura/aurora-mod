using Terraria.Audio;
using Terraria.GameContent;

namespace Aurora.Content.Projectiles.Explosives;

public class DirtDynamiteProjectile : ModProjectile
{
	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.friendly = true;

		Projectile.width = 12;
		Projectile.height = 12;

		Projectile.timeLeft = 300;
		Projectile.penetrate = -1;

		Projectile.aiStyle = ProjAIStyleID.Explosive;
	}

	public override void AI() {
		base.AI();

		Projectile.velocity.Y += 0.1f;

		if (!Main.rand.NextBool(5)) {
			return;
		}

		Dust.NewDust(Projectile.Center, 0, 0, DustID.Smoke);
		Dust.NewDust(Projectile.Center, 0, 0, DustID.Dirt, 0, 0, 100, Color.Transparent);

		Projectile.netUpdate = true;
	}

	public override void OnKill(int timeLeft) {
		base.OnKill(timeLeft);

		ApplyExplosionEffects();
	}

	public override bool PreDraw(ref Color lightColor) {
		var texture = TextureAssets.Projectile[Type].Value;

		var offsetX = 0;
		var offsetY = 0;

		var originX = 0f;

		ProjectileLoader.DrawOffset(Projectile, ref offsetX, ref offsetY, ref originX);

		var positionOffset = new Vector2(offsetX, offsetY + Projectile.gfxOffY);
		var originOffset = new Vector2(originX, 0f);

		var position = Projectile.Center - Main.screenPosition + positionOffset;
		var origin = texture.Size() / 2f + originOffset;

		Main.EntitySpriteDraw(
			texture,
			position,
			null,
			lightColor,
			Projectile.rotation,
			origin,
			Projectile.scale,
			SpriteEffects.None
		);

		return false;
	}

	private void ApplyExplosionEffects() {
		for (var i = 0; i < 30; i++) {
			var dust = Dust.NewDustDirect(
				Projectile.position,
				Projectile.width,
				Projectile.height,
				31,
				0f,
				0f,
				100,
				Color.Transparent,
				1.5f
			);

			dust.velocity *= 1.4f;
		}

		for (var i = 0; i < 80; i++) {
			var dust = Dust.NewDustDirect(
				Projectile.position,
				Projectile.width,
				Projectile.height,
				DustID.Dirt,
				0f,
				0f,
				100,
				Color.Transparent,
				2.2f
			);

			dust.noGravity = true;

			dust.velocity *= 4f;
			dust.velocity.Y -= 1.2f;

			dust = Dust.NewDustDirect(
				Projectile.position,
				Projectile.width,
				Projectile.height,
				DustID.Dirt,
				0f,
				0f,
				100,
				Color.Transparent,
				1.3f
			);

			dust.velocity *= 2f;
			dust.velocity.Y -= 1.2f;
		}

		for (var i = 1; i <= 2; i++) {
			for (var j = -1; j <= 1; j += 2) {
				for (var k = -1; k <= 1; k += 2) {
					var gore = Gore.NewGoreDirect(
						Projectile.GetSource_Death("Explosion"),
						Projectile.position,
						Vector2.Zero,
						Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1)
					);

					gore.velocity *= i == 1 ? 0.4f : 0.8f;
					gore.velocity += new Vector2(j, k);
				}
			}
		}

		Projectile.Kill_DirtAndFluidProjectiles_RunDelegateMethodPushUpForHalfBricks(
			Projectile.Center.ToTileCoordinates(),
			10f,
			DelegateMethods.SpreadDirt
		);

		Projectile.Resize(250, 250);

		SoundEngine.PlaySound(in SoundID.Item14, Projectile.Center);
	}
}
