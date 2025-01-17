﻿namespace Aurora.Content.Items.Everbloom;

[AutoloadEquip(EquipType.Head)]
public class EverbloomHoodItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.defense = 1;

        Item.width = 22;
        Item.height = 20;
        
        Item.rare = ItemRarityID.Blue;
    }
}
