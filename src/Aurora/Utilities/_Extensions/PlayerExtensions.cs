using System.Runtime.CompilerServices;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnderwater(this Player player, bool includeSlopes = false) {
        return Collision.DrownCollision(player.position, player.width, player.height, player.gravDir, includeSlopes);
    }

    /// <summary>
    ///		Checks whether the player is on the ground or not.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns><c>true</c> if the player is on the ground; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsGrounded(this Player player) {
        return player.velocity.Y == 0f;
    }

    /// <summary>
    ///		Checks whether the player was on the ground or not.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns><c>true</c> if the player was on the ground; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool WasGrounded(this Player player) {
        return player.oldVelocity.Y == 0f;
    }
}
