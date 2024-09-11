namespace Aurora.Content.Items.Wildlife;

[AutoloadEquip(EquipType.Head)]
public class WildWarriorMaskItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 24;
        Item.height = 22;

        Item.defense = 1;
                
        Item.rare = ItemRarityID.Blue;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs) {
	    return body.type == ModContent.ItemType<WildWarriorGarbItem>() && legs.type == ModContent.ItemType<WildWarriorGreavesItem>();
    }

    public override void UpdateEquip(Player player) {
	    base.UpdateEquip(player);
	    
	    player.GetDamage(DamageClass.Ranged) += 0.025f;
    }

    public override void UpdateArmorSet(Player player) {
	    base.UpdateArmorSet(player);

	    player.setBonus = this.GetLocalizedValue("SetBonus");

	    if (!player.TryGetModPlayer(out WildWarriorPlayer modPlayer)) {
		    return;
	    }

	    modPlayer.Enabled = true;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>(2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
