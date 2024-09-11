namespace Aurora.Content.Items.Everbloom;

[AutoloadEquip(EquipType.Legs)]
public class EverbloomLeggingsItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.defense = 2;

        Item.width = 26;
        Item.height = 18;
                
        Item.rare = ItemRarityID.Blue;
    }
}
