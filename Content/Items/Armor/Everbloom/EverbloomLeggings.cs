﻿namespace Aurora.Content.Items.Armor.Everbloom;

[AutoloadEquip(EquipType.Legs)]
public class EverbloomLeggings : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.defense = 2;

        Item.width = 26;
        Item.height = 18;
    }
}