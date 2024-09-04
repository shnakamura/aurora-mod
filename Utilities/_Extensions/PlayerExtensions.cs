namespace Aurora.Utilities;

/// <summary>
///     Provides <see cref="Player" /> extension methods.
/// </summary>
public static class PlayerExtensions
{
    /// <summary>
    ///     Checks whether the player is underwater or not.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns><c>true</c> if the player is underwater; otherwise, <c>false</c>.</returns>
    public static bool IsUnderwater(this Player player, bool includeSlopes = false) {
        return Collision.DrownCollision(player.position, player.width, player.height, player.gravDir, includeSlopes);
    }

    public static bool IsGrounded(this Player player) {
        return player.velocity.Y == 0f;
    }

    public static bool WasGrounded(this Player player) {
        return player.oldVelocity.Y == 0f;
    }
}
