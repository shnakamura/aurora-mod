using Aurora.Common.Projectiles.Components;
using Aurora.Utilities;

namespace Aurora.Common.Projectiles.Behavior;

public sealed class HorizontalRotation : ProjectileComponent
{
    public struct RotationData
    {
        public float Multiplier;

        public RotationData(float multiplier) {
            Multiplier = multiplier;
        }
    }

    public RotationData Data = new(0.1f);
    
    public override void AI(Projectile projectile) {
        base.AI(projectile);

        if (!Enabled || !projectile.AbleToUpdateVelocity()) {
            return;
        }

        projectile.rotation += projectile.velocity.X * Data.Multiplier;
    }
}
