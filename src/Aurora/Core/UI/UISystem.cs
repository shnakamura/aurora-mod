using System.Collections.Generic;
using Terraria.UI;

namespace Aurora.Core.UI;

[Autoload(Side = ModSide.Client)]
public sealed partial class UISystem : ModSystem
{
	// Terraria doesn't provide any game time instance during rendering, so we keep track of it ourselves.
	private static GameTime? lastGameTime;

	private static List<UIStateContainer>? data = new();

	public override void Unload() {
		base.Unload();

		for (var i = 0; i < data.Count; i++) {
			var data = UISystem.data[i];

			data.UserInterface?.SetState(null);
			data.UserInterface = null;
		}

		data?.Clear();
		data = null;
	}

	public override void UpdateUI(GameTime gameTime) {
		base.UpdateUI(gameTime);

		for (var i = 0; i < data.Count; i++) {
			var state = data[i];

			state.UserInterface.Update(gameTime);
		}

		lastGameTime = gameTime;
	}

	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
		base.ModifyInterfaceLayers(layers);

		for (var i = 0; i < data.Count; i++) {
			var state = data[i];

			var index = layers.FindIndex(l => l.Name == state.Data.Layer);

			if (index < 0) {
				continue;
			}

			var layer = new LegacyGameInterfaceLayer(
				state.Data.Identifier,
				() => {
					state.UserInterface.Draw(Main.spriteBatch, lastGameTime);
					return true;
				},
				state.Data.Type
			);

			layers.Insert(index + state.Data.Offset, layer);
		}
	}

	public static void Register(string identifier, string layer, UIState? value, int offset = 0, InterfaceScaleType type = InterfaceScaleType.UI) {
		var index = data.FindIndex(s => s.Data.Identifier == identifier);
		var container = new UIStateContainer(new UIStateData(identifier, layer, offset, type), value);
		
		container.UserInterface.SetState(value);

		if (index < 0) {
			UISystem.data.Add(container);
		}
		else {
			UISystem.data[index] = container;
		}
	}

	public static bool TryRefresh(string identifier, UIState? value) {
		var index = data.FindIndex(s => s.Data.Identifier == identifier);

		if (index < 0) {
			return false;
		}
		
		var container = UISystem.data[index];
		
		container.UserInterface.SetState(value);
		container.UserInterface.CurrentState.Activate();
		
		return true;
	}

	public static bool TryEnable(string identifier) {
		var index = data.FindIndex(s => s.Data.Identifier == identifier);

		if (index < 0) {
			return false;
		}

		var container = UISystem.data[index];

		container.UserInterface.SetState(container.Value);
		container.UserInterface.CurrentState.Activate();

		return true;
	}

	public static bool TryEnableOrRegister(
		string identifier,
		string layer,
		UIState? value,
		int offset = 0,
		InterfaceScaleType type = InterfaceScaleType.UI,
		bool refresh = false
	) {
		var index = data.FindIndex(s => s.Data.Identifier == identifier);

		if (index < 0) {
			Register(identifier, layer, value, offset, type);
		}

		if (refresh) {
			TryRefresh(identifier, value);
		}

		return TryEnable(identifier);
	}

	public static bool TryDisable(string identifier) {
		var index = data.FindIndex(s => s.Data.Identifier == identifier);

		if (index < 0) {
			return false;
		}

		var container = UISystem.data[index];

		container.UserInterface.CurrentState.Deactivate();
		container.UserInterface.SetState(null);
		
		return true;
	}

	public static bool TryToggle(string identifier) {
		var index = data.FindIndex(s => s.Data.Identifier == identifier);

		if (index < 0) {
			return false;
		}
		
		return data[index].Enabled ? TryDisable(identifier) : TryEnable(identifier);
	}

	public static bool TryToggleOrRegister(
		string identifier,
		string layer,
		UIState? value,
		int offset = 0,
		InterfaceScaleType type = InterfaceScaleType.UI,
		bool refresh = false
	) {
		var index = data.FindIndex(s => s.Data.Identifier == identifier);

		if (index < 0) {
			Register(identifier, layer, value, offset, type);
		}

		if (refresh) {
			TryRefresh(identifier, value);
		}

		return TryToggle(identifier);
	}
}
