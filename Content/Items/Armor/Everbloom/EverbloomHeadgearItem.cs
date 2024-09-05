namespace Aurora.Content.Items.Armor.Everbloom;

[AutoloadEquip(EquipType.Head)]
public class EverbloomHeadgearItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();
        
        Item.defense = 2;

        Item.width = 30;
        Item.height = 22;
    }
}
