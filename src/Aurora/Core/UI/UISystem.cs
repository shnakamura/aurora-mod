using System.Collections.Generic;
using Terraria.UI;

namespace Aurora.Core.UI;

[Autoload(Side = ModSide.Client)]
public sealed partial class UISystem : ModSystem
{
	public sealed class UIStateData(string identifier, string layer, UIState? value, int offset = 0, InterfaceScaleType type = InterfaceScaleType.UI)
	{
		public readonly string Identifier = identifier;

		public readonly string Layer = layer;
		
		public readonly int Offset = offset;

		public readonly InterfaceScaleType Type = type;
		
		public readonly UIState? Value = value;
		
		public bool Enabled;

		public UserInterface UserInterface;
	}

	// Terraria doesn't provide any game time instance during rendering, so we keep track of it ourselves.
	private static GameTime? lastGameTime;

	public static List<UIStateData> Data { get; set; } = new();

	public override void Unload() {
		base.Unload();

		for (var i = 0; i < Data.Count; i++) {
			var data = Data[i];

			data.UserInterface?.SetState(null);
			data.UserInterface = null;
		}

		Data?.Clear();
		Data = null;
	}

	public override void UpdateUI(GameTime gameTime) {
		base.UpdateUI(gameTime);

		for (var i = 0; i < Data.Count; i++) {
			var data = Data[i];

			data.UserInterface.Update(gameTime);
		}

		lastGameTime = gameTime;
	}

	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
		base.ModifyInterfaceLayers(layers);

		for (var i = 0; i < Data.Count; i++) {
			var data = Data[i];

			var index = layers.FindIndex(l => l.Name == data.Layer);

			if (index < 0) {
				continue;
			}

			LegacyGameInterfaceLayer layer = new(
				data.Identifier,
				() => {
					data.UserInterface.Draw(Main.spriteBatch, lastGameTime);
					return true;
				}
			);

			layers.Insert(index + data.Offset, layer);
		}
	}

	public static void Register(string identifier, string layer, UIState? value, int offset = 0, InterfaceScaleType type = InterfaceScaleType.UI) {
		var index = Data.FindIndex(s => s.Identifier == identifier);

		var data = new UIStateData(identifier, layer, value, offset, type) {
			UserInterface = new UserInterface()
		};

		data.UserInterface.SetState(value);

		if (index < 0) {
			Data.Add(data);
		}
		else {
			Data[index] = data;
		}
	}

	public static bool TryEnable(string identifier) {
		var index = Data.FindIndex(s => s.Identifier == identifier);

		if (index < 0) {
			return false;
		}

		var data = Data[index];

		data.UserInterface.CurrentState.Activate();
		data.Enabled = true;

		return true;
	}

	public static bool TryEnableOrRegister(
		string identifier,
		string layer,
		UIState? value,
		int offset = 0,
		InterfaceScaleType type = InterfaceScaleType.UI
	) {
		var index = Data.FindIndex(s => s.Identifier == identifier);

		if (index < 0) {
			Register(identifier, layer, value, offset, type);
		}

		return TryEnable(identifier);
	}

	public static bool TryDisable(string identifier) {
		var index = Data.FindIndex(s => s.Identifier == identifier);

		if (index < 0) {
			return false;
		}

		var data = Data[index];

		data.UserInterface.CurrentState.Deactivate();
		data.Enabled = false;

		return true;
	}

	public static bool TryToggle(string identifier, bool refresh = true) {
		var index = Data.FindIndex(s => s.Identifier == identifier);

		if (index < 0) {
			return false;
		}

		return Data[index].Enabled ? TryDisable(identifier) : TryEnable(identifier);
	}

	public static bool TryToggleOrRegister(
		string identifier,
		string layer,
		UIState? value,
		int offset = 0,
		InterfaceScaleType type = InterfaceScaleType.UI
	) {
		var index = Data.FindIndex(s => s.Identifier == identifier);

		if (index < 0) {
			Register(identifier, layer, value, offset, type);
		}

		return TryToggle(identifier);
	}
}
