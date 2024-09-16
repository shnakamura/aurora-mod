using Terraria.UI;

namespace Aurora.Core.UI;

public struct UIStateContainer(UIStateData data, UIState? value)
{
	public readonly UIStateData Data = data;
	
	public readonly UIState Value = value;

	public UserInterface UserInterface = new();

	public bool Enabled => UserInterface.CurrentState != null;
}
