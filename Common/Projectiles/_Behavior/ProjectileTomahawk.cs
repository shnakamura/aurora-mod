using System.IO;
using Aurora.Core.Projectiles;
using Terraria.ModLoader.IO;

namespace Aurora.Common.Projectiles;

public sealed class ProjectileTomahawk : ProjectileComponent
{
    public struct DurationData
    {
        public int Ticks;

        public DurationData(int ticks) {
            Ticks = ticks;
        }
    }

    private const byte AlphaStep = 15;

    public DurationData HitCooldown = new(60);

    public override void SetDefaults(Projectile entity) {
        base.SetDefaults(entity);

        if (!Enabled) {
            return;
        }

        entity.tileCollide = true;
        entity.friendly = true;

        entity.usesLocalNPCImmunity = true;
        entity.localNPCHitCooldown = HitCooldown.Ticks;

        entity.penetrate = -1;
    }

    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter) {
        base.SendExtraAI(projectile, bitWriter, binaryWriter);

        if (!Enabled) {
            return;
        }

        binaryWriter.Write(HitCooldown.Ticks);
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader) {
        base.ReceiveExtraAI(projectile, bitReader, binaryReader);

        if (!Enabled) {
            return;
        }

        HitCooldown.Ticks = binaryReader.ReadInt32();
    }

    public override bool ShouldUpdatePosition(Projectile projectile) {
        return !Enabled ? base.ShouldUpdatePosition(projectile) : !GetStickyFlag(projectile);
    }

    public override void AI(Projectile projectile) {
        base.AI(projectile);

        if (!Enabled) {
            return;
        }

        UpdateFadeOut(projectile);

        projectile.alpha = (byte)MathHelper.Clamp(projectile.alpha, 0, byte.MaxValue);

        UpdateGravityAI(projectile);
        UpdateStickyAI(projectile);
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone) {
        base.OnHitNPC(projectile, target, hit, damageDone);

        if (!Enabled) {
            return;
        }

        var canStick = !GetStickyFlag(projectile);

        if (!canStick) {
            return;
        }

        var owner = Main.player[projectile.owner];

        if (!owner.active) {
            return;
        }

        owner.heldProj = -1;

        ref var index = ref projectile.ai[2];

        index = target.whoAmI;

        // TODO: Make this have a more subtle movement.
        projectile.velocity = (target.Center - projectile.Center) * 0.75f;

        SetStickyFlag(projectile, true);
    }

    public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity) {
        if (Enabled) {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
        }

        return base.OnTileCollide(projectile, oldVelocity);
    }

    private static void UpdateFadeOut(Projectile projectile) {
        if (projectile.alpha >= byte.MaxValue || projectile.timeLeft > byte.MaxValue / AlphaStep) {
            return;
        }

        projectile.alpha += AlphaStep;
    }

    private static void UpdateGravityAI(Projectile projectile) {
        if (GetStickyFlag(projectile)) {
            return;
        }

        projectile.velocity.Y += 0.2f;
        projectile.rotation += projectile.velocity.X * 0.1f;
    }

    private static void UpdateStickyAI(Projectile projectile) {
        if (!GetStickyFlag(projectile)) {
            return;
        }

        ref var index = ref projectile.ai[2];

        var target = Main.npc[(int)index];

        if (!target.CanBeChasedBy() && target.type != NPCID.TargetDummy) {
            projectile.Kill();
            return;
        }

        projectile.tileCollide = false;

        projectile.gfxOffY = target.gfxOffY;
        projectile.Center = Vector2.Lerp(projectile.Center, target.Center - projectile.velocity, 0.3f);
    }

    private static bool GetStickyFlag(Projectile projectile) {
        return projectile.ai[0] == 1f;
    }

    private static void SetStickyFlag(Projectile projectile, bool value) {
        projectile.ai[0] = value ? 1f : 0f;

        projectile.netUpdate = true;
    }
}
