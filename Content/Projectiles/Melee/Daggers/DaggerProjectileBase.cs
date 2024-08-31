using Terraria.Enums;

namespace Aurora.Content.Projectiles.Melee.Daggers;

public abstract class DaggerProjectileBase : ModProjectile
{
    public const int FadeInDuration = 7;
    public const int FadeOutDuration = 4;

    public const int TotalDuration = 16;

    public float CollisionWidth => 10f * Projectile.scale;

    /// <summary>
    ///     The behavior timer of this projectile.
    /// </summary>
    public int Timer {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    /// <summary>
    ///     Whether this projectile is sticking to an NPC or not.
    /// </summary>
    public bool Sticking {
        get => Projectile.ai[1] != 0;
        set => Projectile.ai[1] = value ? 1 : 0;
    }

    /// <summary>
    ///     The index of this projectile's target within the NPC array.
    /// </summary>
    /// s
    public int Target {
        get => (int)Projectile.ai[2];
        set => Projectile.ai[2] = value;
    }

    public override void SetDefaults() {
        base.SetDefaults();
        
        Projectile.DamageType = DamageClass.Melee;

        Projectile.ownerHitCheck = true;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.hide = true;

        Projectile.timeLeft = 360;
        
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.extraUpdates = 1; 
    }
    
    public override bool ShouldUpdatePosition() {
        return false;
    }

    public override void AI() {
        base.AI();

        if (Sticking) {
            var target = Main.npc[Target];

            if (!target.active) {
                Projectile.Kill();
                return;
            }

            Projectile.velocity = target.velocity;
            
            Timer++;
            
            Projectile.Opacity = 1f;
            Projectile.hide = false;

            if (Timer >= 60) {
                target.SimpleStrikeNPC(5, 0);
                
                Timer = 0;
            }

            Projectile.position += Projectile.velocity *= 0.5f;
        }
        else {
            var player = Main.player[Projectile.owner];

            Timer++;

            if (Timer >= TotalDuration) {
                Projectile.Kill();
                return;
            }

            player.heldProj = Projectile.whoAmI;

            var opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, true);
            var multiplier = Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, true);

            Projectile.Opacity = opacity * multiplier;

            var center = player.RotatedRelativePoint(player.MountedCenter, false, false);
            
            Projectile.Center = center + Projectile.velocity * (Timer - 1f);

            Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
           
            SetVisualOffsets();
        }
    }

    public override void CutTiles() {
        base.CutTiles();

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
        base.OnHitNPC(target, hit, damageDone);

        Timer = 0;
        Sticking = true;
        Target = target.whoAmI;

        Projectile.timeLeft = 360;
    }

    private void SetVisualOffsets() {
        const int HalfSpriteWidth = 24 / 2;
        const int HalfSpriteHeight = 24 / 2;

        var halfProjWidth = Projectile.width / 2;
        var halfProjHeight = Projectile.height / 2;

        DrawOriginOffsetX = 0;
        DrawOffsetX = -(HalfSpriteWidth - halfProjWidth);
        DrawOriginOffsetY = -(HalfSpriteHeight - halfProjHeight);
    }
}
