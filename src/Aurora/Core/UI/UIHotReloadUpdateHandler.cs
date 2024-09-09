using System.Reflection.Metadata;
using Aurora.Core.UI;
using Terraria.UI;

[assembly: MetadataUpdateHandler(typeof(UIHotReloadUpdateHandler))]

namespace Aurora.Core.UI;

internal static class UIHotReloadUpdateHandler
{
	// Despite not having any implementation, this method is still required for the handler to work properly.
	internal static void ClearCache(Type[]? types) { }

	internal static void UpdateApplication(Type[]? updatedTypes) {
		Main.QueueMainThreadAction(
			() => {
				foreach (var type in updatedTypes) {
					if (!typeof(UIState).IsAssignableFrom(type) && !typeof(UIElement).IsAssignableFrom(type)) {
						continue;
					}

					UISystem.RefreshAllStates();
				}
			}
		);
	}
}
