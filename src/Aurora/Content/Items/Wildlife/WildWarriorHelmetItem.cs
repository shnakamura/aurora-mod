namespace Aurora.Content.Items.Wildlife;

[AutoloadEquip(EquipType.Head)]
public class WildWarriorHelmetItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 22;
        Item.height = 16;

        Item.defense = 2;
                
        Item.rare = ItemRarityID.Blue;
    }
    
    public override bool IsArmorSet(Item head, Item body, Item legs) {
	    return body.type == ModContent.ItemType<WildWarriorGarbItem>() && legs.type == ModContent.ItemType<WildWarriorGreavesItem>();
    }
    
    public override void UpdateEquip(Player player) {
	    base.UpdateEquip(player);

	    player.GetDamage(DamageClass.Melee) += 0.025f;
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
