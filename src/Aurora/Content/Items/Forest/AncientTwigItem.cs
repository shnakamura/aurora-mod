namespace Aurora.Content.Items.Forest;

public class AncientTwigItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.width = 26;
        Item.height = 28;

        Item.value = Item.sellPrice(copper: 5);
    }
}
