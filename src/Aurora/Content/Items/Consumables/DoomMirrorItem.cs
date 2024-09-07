using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Aurora.Content.Items.Consumables;

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
    }
}
