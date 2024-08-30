using Aurora.Common.Projectiles.Components;
using Aurora.Utilities;

namespace Aurora.Common.Projectiles.Behavior;

public sealed class Gravity : ProjectileComponent
{
    public struct GravityData
    {
        public float Step;

        public GravityData(float step) {
            Step = step;
        }
    }

    public GravityData Data = new(0.2f);
    
    public override void AI(Projectile projectile) {
        base.AI(projectile);

        if (!Enabled || !projectile.AbleToUpdateVelocity()) {
            return;
        }

        projectile.velocity.Y += Data.Step;
    }
}
