using Aurora.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Aurora.Content.Items.Miscellaneous;

/// <summary>
///		Handles the behavior of removing pylon requirements if the player has a <see cref="PocketCatalystItem"/> in their inventory.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class PocketCatalystItemSystem : ModSystem
{
	public override void Load() {
		base.Load();

		On_Player.InInteractionRange += On_PlayerOnInInteractionRange;

		On_TeleportPylonsSystem.IsPlayerNearAPylon += IsPlayerNearAPylonHook;
		On_TeleportPylonsSystem.DoesPylonHaveEnoughNPCsAroundIt += DoesPylonHaveEnoughNPCsAroundItHook;
	}

	private static bool On_PlayerOnInInteractionRange(
		On_Player.orig_InInteractionRange orig,
		Player self,
		int interactX,
		int interactY,
		TileReachCheckSettings settings
	) {
		var hasCatalyst = self.HasItemInAnyInventory<PocketCatalystItem>();

		return orig(self, interactX, interactY, settings) || (settings.Equals(TileReachCheckSettings.Pylons) && hasCatalyst);
	}

	private static bool IsPlayerNearAPylonHook(On_TeleportPylonsSystem.orig_IsPlayerNearAPylon orig, Player player) {
		var hasCatalyst = player.HasItemInAnyInventory<PocketCatalystItem>();

		return orig(player) || hasCatalyst;
	}

	private static bool DoesPylonHaveEnoughNPCsAroundItHook(
		On_TeleportPylonsSystem.orig_DoesPylonHaveEnoughNPCsAroundIt orig,
		TeleportPylonsSystem self,
		TeleportPylonInfo info,
		int necessaryNPCCount
	) {
		var hasCatalyst = Main.LocalPlayer.HasItemInAnyInventory<PocketCatalystItem>();

		return orig(self, info, necessaryNPCCount) || hasCatalyst;
	}
}
