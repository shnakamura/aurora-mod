namespace Aurora.Content.Items.Miscellaneous;

public class StoneBootsItem : ModItem
{
	public override void SetDefaults() {
		base.SetDefaults();

		Item.accessory = true;

		Item.width = 32;
		Item.height = 28;
	}

	public override void UpdateAccessory(Player player, bool hideVisual) {
		base.UpdateAccessory(player, hideVisual);

		if (!player.controlDown) {
			return;
		}

		player.armorEffectDrawOutlines = !hideVisual;

		player.maxFallSpeed += 5f;
		player.gravity += Player.defaultGravity;
	}
}
