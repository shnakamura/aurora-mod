using System.IO;
using Aurora.Core.Projectiles;
using Terraria.ModLoader.IO;

namespace Aurora.Common.Projectiles;

/// <summary>
///     Provides a component that handles the behavior of a projectile that should stick to NPCs and/or tiles.
/// </summary>
public sealed class ProjectileSticky : ProjectileComponent
{
    public struct StickyData
    {
        /// <summary>
        ///     The maximum amount of projectiles that can stick to an NPC at once.
        /// </summary>
        public int NPCMaxStack {
            get => _npcMaxStack;
            set {
                _npcMaxStack = value;

                Javelins = new Point[_npcMaxStack];
            }
        }

        private int _npcMaxStack;
        
        /// <summary>
        ///     The flags of what the projectile can stick to or not.
        /// </summary>
        public ProjectileStickyFlags Flags;

        /// <summary>
        ///     The point array holding for sticking javelins.
        /// </summary>
        public Point[] Javelins;

        /// <summary>
        ///     Whether the projectile can stick to NPCs or not.
        /// </summary>
        public bool CanStickToNPCs => (Flags & (ProjectileStickyFlags.NPCs | ProjectileStickyFlags.All)) != 0;

        /// <summary>
        ///     Whether the projectile can stick to tiles or not.
        /// </summary>
        public bool CanStickToTiles => (Flags & (ProjectileStickyFlags.Tiles | ProjectileStickyFlags.All)) != 0;

        public StickyData(ProjectileStickyFlags flags, int npcMaxStack) {
            Flags = flags;
            NPCMaxStack = npcMaxStack;
        }
    }

    public delegate void NPCStickCallback(Projectile? projectile, NPC? npc);

    public delegate void TileStickCallback(Projectile? projectile);

    private int Index {
        get => _index;
        set => _index = (int)MathHelper.Clamp(value, 0, Main.maxNPCs);
    }

    private int _index;
    
    /// <summary>
    ///     The offset calculated to position the projectile attached to this component when it's sticking to an NPC.
    /// </summary>
    public Vector2 Offset { get; private set; }

    /// <summary>
    ///     Whether the projectile is sticking to an NPC or not.
    /// </summary>
    public bool IsStickingToNPC { get; private set; }

    /// <summary>
    ///     Whether the projectile is sticking to a tile or not.
    /// </summary>
    public bool IsStickingToTile { get; private set; }

    /// <summary>
    ///     Whether the projectile is sticking to anything or not.
    /// </summary>
    public bool IsStickingToAnything => IsStickingToNPC || IsStickingToTile;

    /// <summary>
    ///     The sticky data parameters associated with the projectile.
    /// </summary>
    public StickyData Data = new(ProjectileStickyFlags.None, 5);

    /// <summary>
    ///     Invoked when the projectile sticks to an NPC.
    /// </summary>
    public event NPCStickCallback? OnStickToNPC;

    /// <summary>
    ///     Invoked when the projectile sticks to a tile.
    /// </summary>
    public event TileStickCallback? OnStickToTile;

    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter) {
        base.SendExtraAI(projectile, bitWriter, binaryWriter);

        if (!Enabled) {
            return;
        }

        binaryWriter.Write(IsStickingToTile);
        binaryWriter.Write(IsStickingToNPC);

        binaryWriter.Write(Index);
        
        binaryWriter.WriteVector2(Offset);
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader) {
        base.ReceiveExtraAI(projectile, bitReader, binaryReader);

        if (!Enabled) {
            return;
        }

        IsStickingToTile = binaryReader.ReadBoolean();
        IsStickingToNPC = binaryReader.ReadBoolean();
        
        Index = binaryReader.ReadInt32();
        
        Offset = binaryReader.ReadVector2();
    }

    public override void AI(Projectile projectile) {
        base.AI(projectile);
        
        if (!Enabled) {
            return;
        }

        UpdateNPCStick(projectile);
        UpdateTileStick(projectile);
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone) {
        base.OnHitNPC(projectile, target, hit, damageDone);

        if (!Enabled) {
            return;
        }

        var canStick = !IsStickingToNPC && !IsStickingToTile && Data.CanStickToNPCs;

        if (!canStick) {
            return;
        }

        Offset = target.Center - projectile.Center + (projectile.velocity * 0.75f);

        Index = target.whoAmI;

        IsStickingToNPC = true;

        OnStickToNPC?.Invoke(projectile, target);
    }

    public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity) {
        if (!Enabled) {
            return base.OnTileCollide(projectile, oldVelocity);
        }

        var canStick = !IsStickingToTile && !IsStickingToNPC && Data.CanStickToTiles;

        if (!canStick) {
            return false;
        }
        
        IsStickingToTile = true;

        OnStickToTile?.Invoke(projectile);

        return false;
    }

    private void UpdateNPCStick(Projectile projectile) {
        if (!IsStickingToNPC) {
            return;
        }

        var target = Main.npc[Index];

        // TODO: This should trigger effects such as fade-outs, etc.
        if (!target.active) {
            return;
        }

        projectile.tileCollide = false;

        projectile.Center = target.Center - Offset;
        projectile.gfxOffY = target.gfxOffY;
    }

    private void UpdateTileStick(Projectile projectile) {
        if (!IsStickingToTile) {
            return;
        }

        projectile.velocity *= 0.5f;
    }
}
