using Aurora.Core.Items;
using Terraria.GameContent;

namespace Aurora.Common.Items;

[Autoload(Side = ModSide.Client)]
public sealed class ItemBulletCasings : ItemComponent
{
    public struct GoreData
    {
        /// <summary>
        ///     The type of the gore to spawn.
        /// </summary>
        /// <remarks>
        ///     Defaults to <c>-1</c>.
        /// </remarks>
        public int Type = -1;

        /// <summary>
        ///     The amount of gore to spawn.
        /// </summary>
        /// <remarks>
        ///     Defaults to <c>1</c>.
        /// </remarks>
        public int Amount = 1;

        public GoreData(int type, int amount) {
            Type = type;
            Amount = amount;
        }
    }

    /// <summary>
    ///     The parameters used to spawn bullet casings for the item.
    /// </summary>
    public GoreData Data = new(-1, 1);

    public override bool? UseItem(Item item, Player player) {
        if (!Enabled || Data.Type < 0 || Data.Amount <= 0) {
            return base.UseItem(item, player);
        }

        var texture = TextureAssets.Gore[Data.Type].Value;
        var position = player.Center + new Vector2(12f * player.direction, -6f);

        if (player.direction == -1) {
            // Values are hardcoded because the offset is different based on the player's direction.
            position -= texture.Size() / 2f + new Vector2(12f, 0f);
        }

        for (var i = 0; i < Data.Amount; i++) {
            var velocity = new Vector2(Main.rand.NextFloat(0.75f, 1f) * -player.direction, -Main.rand.NextFloat(1f, 1.5f)) + player.velocity / 2f;

            Gore.NewGore(player.GetSource_ItemUse(item), position, velocity, Data.Type);
        }

        return base.UseItem(item, player);
    }
}
