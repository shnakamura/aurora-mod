namespace Aurora.Content.Items.Materials;

public class NutItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.width = 22;
        Item.height = 26;

        Item.value = Item.sellPrice(copper: 1);
    }
}
