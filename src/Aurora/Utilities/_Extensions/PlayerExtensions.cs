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

    /// <summary>
    ///		Checks whether the player has a specified item in any inventory or not.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <typeparam name="T">The type of the item to check.</typeparam>
    /// <returns><c>true</c> if the player has the specified item in any inventory; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasItemInAnyInventory<T>(this Player player) where T : ModItem {
	    return player.HasItemInAnyInventory(ModContent.ItemType<T>());
    }
}
