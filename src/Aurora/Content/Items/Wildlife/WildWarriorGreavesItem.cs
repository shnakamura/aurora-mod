namespace Aurora.Content.Items.Wildlife;

[AutoloadEquip(EquipType.Legs)]
public class WildWarriorGreavesItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 22;
        Item.height = 16;

        Item.defense = 1;
                
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateEquip(Player player) {
	    base.UpdateEquip(player);

	    player.moveSpeed += 0.05f;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<WildlifeFragmentItem>()
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
