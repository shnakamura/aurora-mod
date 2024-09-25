using Aurora.Content.Projectiles.Everbloom;

namespace Aurora.Content.Buffs;

public class BloomBulbBuff : ModBuff
{
	public override void SetStaticDefaults() {
		base.SetStaticDefaults();

		Main.buffNoSave[Type] = true;
		Main.buffNoTimeDisplay[Type] = true;
	}

	public override void Update(Player player, ref int buffIndex) {
		base.Update(player, ref buffIndex);

		if (player.ownedProjectileCounts[ModContent.ProjectileType<BloomBulbProjectile>()] > 0) {
			player.buffTime[buffIndex] = 18000;
		}
		else {
			player.DelBuff(buffIndex--);
		}
	}
}
