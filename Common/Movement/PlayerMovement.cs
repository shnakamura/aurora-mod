namespace Aurora.Common.Movement;

// The game doesn't set the player's old velocity, so we have to do it ourselves.
public sealed class PlayerMovement : ModPlayer
{
    public override void PostUpdate() {
        base.PostUpdate();

        Player.oldVelocity = Player.velocity;
    }
}
