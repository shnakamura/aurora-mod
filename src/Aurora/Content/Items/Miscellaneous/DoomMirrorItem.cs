using Aurora.Core.EC;
using Aurora.Core.Graphics;

namespace Aurora.Content.Items.Miscellaneous;

public class DoomMirrorItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;

        Item.width = 36;
        Item.height = 34;

        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.HoldUp;

        Item.rare = ItemRarityID.Blue;
    }

    public override bool CanUseItem(Player player) {
	    return player.lastDeathPostion != Vector2.Zero;
    }

    public override bool? UseItem(Player player) {
	    player.Teleport(player.lastDeathPostion - player.Size / 2f, TeleportationStyleID.DebugTeleport);
	    player.velocity = Vector2.Zero;

	    return true;
    }
}
