using System.Collections.Generic;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Aurora.Content.Projectiles.Beach;

public class CrabPincersProjectile : ModProjectile
{
	/// <summary>
	///     The mod-qualified path to <see cref="EyesTexture" />.
	/// </summary>
	public const string EyesTexturePath = $"{nameof(Aurora)}/Assets/Textures/Projectiles/Beach/{nameof(CrabPincersProjectile)}_Eyes";

	/// <summary>
	///     The mod-qualified path to <see cref="LeftPincerTexture" />.
	/// </summary>
	public const string LeftPincerTexturePath = $"{nameof(Aurora)}/Assets/Textures/Projectiles/Beach/{nameof(CrabPincersProjectile)}_Left";

	/// <summary>
	///     The mod-qualified path to <see cref="RightPincerTexture" />.
	/// </summary>
	public const string RightPincerTexturePath = $"{nameof(Aurora)}/Assets/Textures/Projectiles/Beach/{nameof(CrabPincersProjectile)}_Right";

	/// <summary>
	///     The asset that holds the texture used for rendering this projectile's eyes.
	/// </summary>
	public static Asset<Texture2D> EyesTexture { get; private set; }

	/// <summary>
	///     The asset that holds the texture used for rendering this projectile's left pincer.
	/// </summary>
	public static Asset<Texture2D> LeftPincerTexture { get; private set; }

	/// <summary>
	///     The asset that holds the texture used for rendering this projectile's right pincer.
	/// </summary>
	public static Asset<Texture2D> RightPincerTexture { get; private set; }

	private float leftRotation;
	private float rightRotation;

	private float scale;

	public override void Load() {
		base.Load();

		EyesTexture = ModContent.Request<Texture2D>(EyesTexturePath);
		LeftPincerTexture = ModContent.Request<Texture2D>(LeftPincerTexturePath);
		RightPincerTexture = ModContent.Request<Texture2D>(RightPincerTexturePath);
	}

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();

		ProjectileID.Sets.MinionSacrificable[Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Type] = true;
	}

	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.usesLocalNPCImmunity = true;
		Projectile.netImportant = true;
		Projectile.ignoreWater = true;
		Projectile.friendly = true;
		Projectile.sentry = true;
		Projectile.hide = true;

		Projectile.width = 50;
		Projectile.height = 30;

		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.localNPCHitCooldown = 30;

		Projectile.timeLeft = Projectile.SentryLifeTime;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
		base.OnHitNPC(target, hit, damageDone);

		leftRotation += MathHelper.ToRadians(30f);
		rightRotation -= MathHelper.ToRadians(30f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity) {
		return false;
	}

	public override void OnSpawn(IEntitySource source) {
		base.OnSpawn(source);

		var tileCoordinates = Projectile.position.ToTileCoordinates();
		var collisionPosition = new Vector2(tileCoordinates.X, tileCoordinates.Y + 1f) * 16f;

		Collision.HitTiles(collisionPosition, Projectile.velocity, Projectile.width, Projectile.height);
	}

	public override void AI() {
		base.AI();

		Projectile.velocity.Y += 0.3f;

		scale = MathHelper.Lerp(scale, 1f, 0.1f);

		leftRotation = leftRotation.AngleLerp(0f, 0.1f);
		rightRotation = rightRotation.AngleLerp(0f, 0.1f);
	}

	public override void DrawBehind(
		int index,
		List<int> behindNPCsAndTiles,
		List<int> behindNPCs,
		List<int> behindProjectiles,
		List<int> overPlayers,
		List<int> overWiresUI
	) {
		base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);

		behindNPCsAndTiles.Add(index);
	}

	public override bool PreDraw(ref Color lightColor) {
		base.PreDraw(ref lightColor);

		var offsetX = 0;
		var offsetY = 0;

		var originX = 0f;

		ProjectileLoader.DrawOffset(Projectile, ref offsetX, ref offsetY, ref originX);

		var positionOffset = new Vector2(offsetX, offsetY + Projectile.gfxOffY);
		var originOffset = new Vector2(originX, 0f);

		var position = Projectile.Center - Main.screenPosition + positionOffset;

		var eyesTexture = EyesTexture.Value;

		Main.EntitySpriteDraw(
			eyesTexture,
			position + new Vector2(0f, eyesTexture.Height),
			null,
			lightColor,
			0f,
			new Vector2(eyesTexture.Width / 2f, eyesTexture.Height) + originOffset,
			new Vector2(1f, scale),
			SpriteEffects.None
		);

		var leftPincerTexture = LeftPincerTexture.Value;

		Main.EntitySpriteDraw(
			leftPincerTexture,
			position + new Vector2(-24f, leftPincerTexture.Height / 2f),
			null,
			lightColor,
			leftRotation,
			new Vector2(leftPincerTexture.Width / 2f, leftPincerTexture.Height) + originOffset,
			new Vector2(1f, scale),
			SpriteEffects.None
		);

		var rightPincerTexture = RightPincerTexture.Value;

		Main.EntitySpriteDraw(
			rightPincerTexture,
			position + new Vector2(24f, rightPincerTexture.Height / 2f),
			null,
			lightColor,
			rightRotation,
			new Vector2(rightPincerTexture.Width / 2f, rightPincerTexture.Height) + originOffset,
			new Vector2(1f, scale),
			SpriteEffects.None
		);

		return true;
	}
}
