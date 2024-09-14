namespace Aurora.Core.UI;

[Autoload(Side = ModSide.Client)]
public sealed partial class UISystem : ModSystem
{
	internal static bool TryRefresh(string identifier) {
		var index = UISystem.data.FindIndex(s => s.Identifier == identifier);

		if (index < 0) {
			return false;
		}

		var data = UISystem.data[index];

		data.Value.RemoveAllChildren();

		data.Value.OnActivate();
		data.Value.OnInitialize();

		data.UserInterface.SetState(null);
		data.UserInterface.SetState(data.Value);

		return true;
	}

	internal static void RefreshAllStates() {
		for (var i = 0; i < data.Count; i++) {
			TryRefresh(data[i].Identifier);
		}
	}
}
