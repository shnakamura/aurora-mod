namespace Aurora.Common.Projectiles;

[Flags]
public enum ProjectileStickyFlags : byte
{
    /// <summary>
    ///     The projectile can not stick to anything.
    /// </summary>
    None = 0,
        
    /// <summary>
    ///     The projectile can stick to NPCs.
    /// </summary>
    NPCs = 0 << 0,
        
    /// <summary>
    ///     The projectile can stick to tiles.
    /// </summary>
    Tiles = 1 << 1,
        
    /// <summary>
    ///     The projectile can stick to tiles and NPCs.
    /// </summary>
    All = NPCs | Tiles
}