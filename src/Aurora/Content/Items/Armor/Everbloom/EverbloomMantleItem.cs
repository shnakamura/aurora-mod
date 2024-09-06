namespace Aurora.Content.Items.Armor.Everbloom;

[AutoloadEquip(EquipType.Body)]
public class EverbloomMantleItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.defense = 3;

        Item.width = 34;
        Item.height = 18;
    }
}
