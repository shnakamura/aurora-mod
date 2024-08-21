using System.IO;
using Aurora.Common.Projectiles.Components;
using Terraria.ModLoader.IO;

namespace Aurora.Common.Projectiles.Behavior;

/// <summary>
///     Provides behavior of a projectile that should stick to NPCs and/or tiles.
/// </summary>
public sealed class ProjectileStickyBehavior : ProjectileComponent
{
    public struct StickyData
    {
        /// <summary>
        ///     The flags of what the projectile can stick to or not.
        /// </summary>
        /// <remarks>
        ///     Defaults to <see cref="ProjectileStickyFlags.None" />.
        /// </remarks>
        public ProjectileStickyFlags Flags { get; set; } = ProjectileStickyFlags.None;

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
        ///     The point array holding for sticking javelins.
        /// </summary>
        public Point[] Javelins { get; private set; }

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
    ///     The sticky data associated with the projectile.
    /// </summary>
    public StickyData Data = new(ProjectileStickyFlags.None, 5);

    /// <summary>
    ///     Invoked when the projectile sticks to an NPC.
    /// </summary>
    public event NPCStickCallback? OnStickToNPC;

    /// <summary>
    ///     Invoked when the projectile sticks to a tile.
    /// </summary>
    public event TileStickCallback OnStickToTile;

    public override GlobalProjectile Clone(Projectile? from, Projectile to) {
        var clone = base.Clone(from, to);

        if (!Enabled || clone is not ProjectileStickyBehavior component) {
            return clone;
        }

        component.IsStickingToTile = IsStickingToTile;
        component.IsStickingToNPC = IsStickingToNPC;

        component.Data = Data;

        component.Index = Index;

        return clone;
    }

    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter) {
        base.SendExtraAI(projectile, bitWriter, binaryWriter);

        if (!Enabled) {
            return;
        }

        binaryWriter.Write(IsStickingToTile);
        binaryWriter.Write(IsStickingToNPC);

        binaryWriter.Write(Index);
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader) {
        base.ReceiveExtraAI(projectile, bitReader, binaryReader);

        if (!Enabled) {
            return;
        }

        IsStickingToTile = binaryReader.ReadBoolean();
        IsStickingToNPC = binaryReader.ReadBoolean();
        
        Index = binaryReader.ReadInt32();
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

        var canStick = !IsStickingToNPC && Data.CanStickToNPCs;

        if (!canStick) {
            return;
        }

        Projectile.KillOldestJavelin(projectile.whoAmI, projectile.type, target.whoAmI, Data.Javelins);

        projectile.velocity = (target.Center - projectile.Center) * 0.75f;

        Index = target.whoAmI;

        IsStickingToNPC = true;

        OnStickToNPC?.Invoke(projectile, target);
    }

    public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity) {
        var value = base.OnTileCollide(projectile, oldVelocity);

        if (!Enabled) {
            return value;
        }

        var canStick = !IsStickingToTile && Data.CanStickToTiles;

        if (!canStick) {
            return value;
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

        if (!target.active) {
            return;
        }

        projectile.tileCollide = false;

        projectile.Center = target.Center - projectile.velocity * 2f;
        projectile.gfxOffY = target.gfxOffY;
    }

    private void UpdateTileStick(Projectile projectile) {
        if (!IsStickingToTile) {
            return;
        }

        projectile.velocity *= 0.5f;
    }
}
