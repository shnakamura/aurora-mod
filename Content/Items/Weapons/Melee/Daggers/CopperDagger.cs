using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Enums;

namespace Aurora.Content.Items.Weapons.Melee.Daggers
{
    public class CopperDagger : ModItem
    {
        public override void SetDefaults() 
        {
            Item.width = 24;
            Item.height = 24;
            Item.damage = 5;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 0, 10);

            Item.shoot = ModContent.ProjectileType<CopperDaggerProjectile>(); // The projectile is what makes a shortsword work
            Item.shootSpeed = 1.9f;
        }
    }

    public class CopperDaggerProjectile : ModProjectile 
    {
        public const int FadeInDuration = 7;
        public const int FadeOutDuration = 4;

        public const int TotalDuration = 16;

        // The "width" of the blade
        public float CollisionWidth => 10f * Projectile.scale;

        public int Timer {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        //public override string Texture => Content.Items.Weapons.Melee.Daggers + "ChestnutLauncher";

        public override void SetDefaults() {
            Projectile.Size = new Vector2(18); // This sets width and height to the same value (important when projectiles can rotate)
            Projectile.aiStyle = -1; // Use our own AI to customize how it behaves, if you don't want that, keep this at ProjAIStyleID.ShortSword. You would still need to use the code in SetVisualOffsets() though
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true; // Prevents hits through tiles. Most melee weapons that use projectiles have this
            Projectile.extraUpdates = 1; // Update 1+extraUpdates times per tick
            Projectile.timeLeft = 360; // This value does not matter since we manually kill it earlier, it just has to be higher than the duration we use in AI
            Projectile.hide = true; // Important when used alongside player.heldProj. "Hidden" projectiles have special draw conditions
        }
        public bool Sticking {
            get { return Projectile.ai[1] != 0; } // Because ai [0] is = 0 by default, != 0 is used here for judgment  
            set { Projectile.ai[1] = value ? 1 : 0; } // Ternary operator: when the expression value is true, return the former, otherwise return the latter 
        }
        public int TargetWho {
            get { return (int)Projectile.ai[2]; }
            set { Projectile.ai[2] = value; }
        }

        int attackTimer = 0;
        public override void AI() {
            if (Sticking) // Executed when the bullet screen sticks to the NPC 
            {
                NPC target = Main.npc[TargetWho];

                if (!target.active) {
                    Projectile.Kill();
                    return;
                }
                //Main.NewText(target.velocity);
                Projectile.velocity = target.velocity;
                attackTimer++;
                Projectile.Opacity = 1f;
                Projectile.hide = false;

               
                if(attackTimer >= 60) {
                    target.SimpleStrikeNPC(5, 0);
                    attackTimer = 0;
                }
                Projectile.position += Projectile.velocity *= 0.5f;
               
            }
            else 
            {
                Player player = Main.player[Projectile.owner];

                Timer += 1;
                if (Timer >= TotalDuration) {
                    
                    Projectile.Kill();
                    return;
                }
                else {
                    
                    player.heldProj = Projectile.whoAmI;
                }
                Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

                Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
                Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);

                Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
                SetVisualOffsets();
            }
        }
        private void SetVisualOffsets() {
            // 24 is the sprite size (here both width and height equal)
            const int HalfSpriteWidth = 24 / 2;
            const int HalfSpriteHeight = 24 / 2;

            int HalfProjWidth = Projectile.width / 2;
            int HalfProjHeight = Projectile.height / 2;

            // Vanilla configuration for "hitbox in middle of sprite"
            DrawOriginOffsetX = 0;
            DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
            DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);         
        }

        public override bool ShouldUpdatePosition() {
            // Update Projectile.Center manually
            return false;
        }

        public override void CutTiles() {
            // "cutting tiles" refers to breaking pots, grass, queen bee larva, etc.
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
            Utils.PlotTileLine(start, end, CollisionWidth, DelegateMethods.CutTiles);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            // "Hit anything between the player and the tip of the sword"
            // shootSpeed is 2.1f for reference, so this is basically plotting 12 pixels ahead from the center
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity * 6f;
            float collisionPoint = 0f; // Don't need that variable, but required as parameter
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref collisionPoint);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            Sticking = true;
            TargetWho = target.whoAmI;
            Projectile.timeLeft = 360;
        }
    }
}
