using Terraria.UI;

namespace Aurora.Core.UI;

// TODO: Redo the implementation to use this instead.
public readonly record struct UIStateData(
	string Identifier, 
	string Layer,
	int Offset = 0,
	InterfaceScaleType Type = InterfaceScaleType.UI
);
