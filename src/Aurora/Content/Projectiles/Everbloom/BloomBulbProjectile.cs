using Aurora.Common.Projectiles;
using Aurora.Content.Buffs;
using Aurora.Core.Projectiles;

namespace Aurora.Content.Projectiles.Everbloom;

public class BloomBulbProjectile : ModProjectile
{
	/// <summary>
	///		The squared attack range of this projectile in pixels.
	/// </summary>
	public const float SquaredAttackRange = 32f * 16f * 32f * 16f;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();

		Main.projPet[Type] = true;
		Main.projFrames[Type] = 2;

		ProjectileID.Sets.CultistIsResistantTo[Type] = true;

		ProjectileID.Sets.MinionSacrificable[Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Type] = true;
	}

	public override void SetDefaults() {
		base.SetDefaults();

		Projectile.tileCollide = false;
		Projectile.friendly = true;
		Projectile.minion = true;

		Projectile.width = 16;
		Projectile.height = 16;

		Projectile.penetrate = -1;
		Projectile.minionSlots = 0.5f;

		Projectile.aiStyle = -1;

		Projectile.TryEnable<ProjectileOwnerBuffCheck>(static c => c.Data.Type = ModContent.BuffType<BloomBulbBuff>());
		Projectile.TryEnable<ProjectileOwnerTeleport>(static c => c.Data.SquaredTeleportRange = 100f * 16f * 100f * 16f);
	}

	public override void AI() {
		base.AI();
	}

	public override bool? CanCutTiles() {
		return false;
	}

	public override bool MinionContactDamage() {
		return false;
	}
}
