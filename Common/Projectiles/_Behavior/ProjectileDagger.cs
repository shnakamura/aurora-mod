using System.IO;
using Aurora.Core.Projectiles;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader.IO;

namespace Aurora.Common.Projectiles;

public sealed class ProjectileDagger : ProjectileComponent
{
    private const byte AlphaStep = 15;
    
    public struct DurationData
    {
        public int Ticks;

        public DurationData(int ticks) {
            Ticks = ticks;
        }
    }

    public DurationData ShortswordDuration = new(15);
    public DurationData ShortswordHitCooldown = new(60);
    
    public DurationData StickyDuration = new(180);

    public int FullDuration => ShortswordDuration.Ticks + StickyDuration.Ticks;
    
    public override void SetDefaults(Projectile entity) {
        base.SetDefaults(entity);

        if (!Enabled) {
            return;
        }
        
        entity.DamageType = DamageClass.Melee;

        entity.ownerHitCheck = true;
        entity.tileCollide = false;
        entity.friendly = true;

        entity.usesLocalNPCImmunity = true;
        entity.localNPCHitCooldown = ShortswordHitCooldown.Ticks;
        
        entity.aiStyle = -1;
        entity.penetrate = -1;
        entity.extraUpdates = 1;
        
        entity.timeLeft = FullDuration;
    }

    public override bool ShouldUpdatePosition(Projectile projectile) {
        return !Enabled ? base.ShouldUpdatePosition(projectile) : false;
    }

    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter) {
        base.SendExtraAI(projectile, bitWriter, binaryWriter);

        if (!Enabled) {
            return;
        }
        
        binaryWriter.Write(ShortswordDuration.Ticks);
        binaryWriter.Write(ShortswordHitCooldown.Ticks);
        
        binaryWriter.Write(StickyDuration.Ticks);
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader) {
        base.ReceiveExtraAI(projectile, bitReader, binaryReader);

        if (!Enabled) {
            return;
        }

        ShortswordDuration.Ticks = binaryReader.ReadInt32();
        ShortswordHitCooldown.Ticks = binaryReader.ReadInt32();
        
        StickyDuration.Ticks = binaryReader.ReadInt32();
    }

    public override void AI(Projectile projectile) {
        base.AI(projectile);

        if (!Enabled) {
            return;
        }
        
        UpdateFadeOut(projectile);

        projectile.alpha = (byte)MathHelper.Clamp(projectile.alpha, 0, byte.MaxValue);

        UpdateShortswordAI(projectile);
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
        projectile.velocity = (target.Center - projectile.Center) * 0.75f - projectile.velocity;

        SetStickyFlag(projectile, true);
    }

    public override void CutTiles(Projectile projectile) {
        base.CutTiles(projectile);

        if (!Enabled) {
            return;
        }
        
        DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
        
        var start = projectile.Center;
        var end = start + projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
        
        Utils.PlotTileLine(start, end, projectile.width, DelegateMethods.CutTiles);
    }

    public override bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox) {
        if (!Enabled) {
            return base.Colliding(projectile, projHitbox, targetHitbox);
        }
        
        var start = projectile.Center;
        var end = start + projectile.velocity * 6f;
        
        var _unused = 0f;
        
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, projectile.width, ref _unused);
    }

    public override bool PreDraw(Projectile projectile, ref Color lightColor) {
        if (!Enabled) {
            return base.PreDraw(projectile, ref lightColor);
        }

        var texture = TextureAssets.Projectile[projectile.type].Value;

        var offsetX = 0;
        var offsetY = 0;
        var originX = 0f;
        
        ProjectileLoader.DrawOffset(projectile, ref offsetX, ref offsetY, ref originX);

        var projectileOffset = new Vector2(offsetX, offsetY);
        var positionOffset = new Vector2(0f, projectile.gfxOffY);
        var position = projectile.Center - Main.screenPosition + projectileOffset + positionOffset;
        
        var frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);

        var originOffset = new Vector2(originX, 0f);
        var origin = texture.Size() / 2f + originOffset;
        
        var effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        
        Main.EntitySpriteDraw(
            texture,
            position,
            frame,
            projectile.GetAlpha(lightColor),
            projectile.rotation,
            origin,
            projectile.scale,
            effects
        );
        
        return false;
    }

    private static void UpdateFadeOut(Projectile projectile) {
        if (projectile.alpha >= byte.MaxValue || projectile.timeLeft > byte.MaxValue / AlphaStep) {
            return;
        }

        projectile.alpha += AlphaStep;
    }

    private void UpdateShortswordAI(Projectile projectile) {
        if (GetStickyFlag(projectile)) {
            return;
        }
        
        ref var timer = ref projectile.ai[1];

        timer++;
        
        var owner = Main.player[projectile.owner];

        if (!owner.active || timer > ShortswordDuration.Ticks) {
            projectile.Kill();
            return;
        }

        owner.heldProj = projectile.whoAmI;

        var center = owner.RotatedRelativePoint(owner.Center, false, false);

        projectile.Center = center + projectile.velocity * (timer - 1f);

        projectile.direction = Math.Sign(projectile.velocity.X);
        projectile.spriteDirection = projectile.direction;
        
        projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * -projectile.spriteDirection;
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
