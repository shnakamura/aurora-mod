using Aurora.Common.Projectiles.Components;
using Terraria.Enums;

namespace Aurora.Common.Projectiles.Behavior;

// TODO: Implementation
public sealed class ProjectileShortsword : ProjectileComponent
{
    public struct ShortswordData
    {
        public ShortswordData() { }
    }

    public ShortswordData Data = new();
    
    public override void AI(Projectile projectile) {
        base.AI(projectile);
        
        if (!Enabled) {
            return;
        }

        var isSticking = projectile.TryGetGlobalProjectile(out ProjectileSticky sticky) && sticky.IsStickingToAnything;

        if (isSticking) {
            return;
        }

        ref var timer = ref projectile.ai[0];
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
}
