namespace Aurora.Utilities.Extensions;

/// <summary>
///     Provides basic <see cref="Player"/> extension methods.
/// </summary>
public static class PlayerExtensions
{
    /// <summary>
    ///     Checks whether the player is in drowning collision or not.
    /// </summary>
    /// <remarks>
    ///     This will only indicate collision, not actual drowning.
    /// </remarks>
    /// <param name="player">The player to check.</param>
    /// <returns><c>true</c> if the player has drowning collision; otherwise, <c>false</c>.</returns>
    public static bool IsDrowning(this Player player) {
        return Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);
    }
}
