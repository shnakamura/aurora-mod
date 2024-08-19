using Terraria.Enums;

namespace Aurora.Content.Projectiles.Melee.Daggers;

public class CopperDagger : ModProjectile
{
    public const int FadeInDuration = 7;
    public const int FadeOutDuration = 4;

    public const int TotalDuration = 16;

    public float CollisionWidth => 10f * Projectile.scale;

    public int Timer {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    public bool Sticking {
        get => Projectile.ai[1] != 0; 
        set => Projectile.ai[1] = value ? 1 : 0; 
    }

    public int TargetWho {
        get => (int)Projectile.ai[2];
        set => Projectile.ai[2] = value;
    }

    private int attackTimer;

    public override void SetDefaults() {
        Projectile.Size = new Vector2(18); // This sets width and height to the same value (important when projectiles can rotate)
        Projectile.aiStyle =
            -1; // Use our own AI to customize how it behaves, if you don't want that, keep this at ProjAIStyleID.ShortSword. You would still need to use the code in SetVisualOffsets() though
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1f;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ownerHitCheck = true; // Prevents hits through tiles. Most melee weapons that use projectiles have this
        Projectile.extraUpdates = 1; // Update 1+extraUpdates times per tick
        Projectile.timeLeft =
            360; // This value does not matter since we manually kill it earlier, it just has to be higher than the duration we use in AI
        Projectile.hide = true; // Important when used alongside player.heldProj. "Hidden" projectiles have special draw conditions
    }

    public override void AI() {
        if (Sticking) 
        {
            var target = Main.npc[TargetWho];

            if (!target.active) {
                Projectile.Kill();
                return;
            }

            Projectile.velocity = target.velocity;
            attackTimer++;
            Projectile.Opacity = 1f;
            Projectile.hide = false;


            if (attackTimer >= 60) {
                target.SimpleStrikeNPC(5, 0);
                attackTimer = 0;
            }

            Projectile.position += Projectile.velocity *= 0.5f;
        }
        else {
            var player = Main.player[Projectile.owner];

            Timer += 1;
            
            if (Timer >= TotalDuration) {
                Projectile.Kill();
                return;
            }

            player.heldProj = Projectile.whoAmI;
            Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, true)
                * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, true);

            var playerCenter = player.RotatedRelativePoint(player.MountedCenter, false, false);
            Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);

            Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
            SetVisualOffsets();
        }
    }

    private void SetVisualOffsets() {
        const int HalfSpriteWidth = 24 / 2;
        const int HalfSpriteHeight = 24 / 2;

        var HalfProjWidth = Projectile.width / 2;
        var HalfProjHeight = Projectile.height / 2;

        DrawOriginOffsetX = 0;
        DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
        DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
    }

    public override bool ShouldUpdatePosition() {
        return false;
    }

    public override void CutTiles() {
        DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
        var start = Projectile.Center;
        var end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
        Utils.PlotTileLine(start, end, CollisionWidth, DelegateMethods.CutTiles);
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
        var start = Projectile.Center;
        var end = start + Projectile.velocity * 6f;
        
        var unused = 0f; 
        
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref unused);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
        Sticking = true;
        TargetWho = target.whoAmI;
        Projectile.timeLeft = 360;
    }
}

