using System.Collections.Generic;
using Aurora.Content.Items.Materials;
using Terraria.DataStructures;

namespace Aurora.Common.Tiles;

public sealed class TileTreeDrops : GlobalTile
{
    /// <summary>
    ///     The list of tree tile types that will drop nuts and ancient twigs when broken.
    /// </summary>
    public static readonly List<int> Types = new() {
        TileID.Trees,
        TileID.PalmTree,
        TileID.PineTree,
        TileID.VanityTreeSakura
    };

    public override void Drop(int i, int j, int type) {
        base.Drop(i, j, type);

        var tile = Framing.GetTileSafely(i, --j);

        if (tile.TileType != TileID.Trees || Framing.GetTileSafely(i, j - 1).HasTile) {
            return;
        }

        var offset = new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
        var position = new Vector2(i, j) * 16f + offset;

        if (Main.rand.NextBool(5)) {
            Item.NewItem(new EntitySource_TileBreak(i, j), position, ModContent.ItemType<NutItem>(), Main.rand.Next(4));
        }

        if (Main.rand.NextBool(10)) {
            Item.NewItem(new EntitySource_TileBreak(i, j), position, ModContent.ItemType<AncientTwigItem>(), Main.rand.Next(4));
        }
    }
}
