using Aurora.Content.Items.Materials;

namespace Aurora.Content.Items.Armor.WildWarrior;

[AutoloadEquip(EquipType.Head)]
public class WildWarriorHelmetItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 22;
        Item.height = 16;

        Item.defense = 2;
    }
    
    public override bool IsArmorSet(Item head, Item body, Item legs) {
	    return body.type == ModContent.ItemType<WildWarriorGarbItem>() && legs.type == ModContent.ItemType<WildWarriorGreavesItem>();
    }
    
    public override void UpdateArmorSet(Player player) {
	    base.UpdateArmorSet(player);

	    player.setBonus = this.GetLocalizedValue("SetBonus");
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>(2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
