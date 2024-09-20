using System.Collections.Generic;
using Aurora.Content.Items.Forest;
using Terraria.DataStructures;

namespace Aurora.Common.Tiles;

/// <summary>
///		Handles the behavior of trees dropping <see cref="NutItem"/> when chopped down.
/// </summary>
public sealed class TileTreeNutDrops : GlobalTile
{
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
