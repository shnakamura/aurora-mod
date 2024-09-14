using Aurora.Core.UI;
using Terraria.Cinematics;

namespace Aurora.Common.Cinematics;

[Autoload(Side = ModSide.Client)]
public sealed class CinematicsSystem : ModSystem
{
	/// <summary>
	///		The identifier of this system's user interface.
	/// </summary>
	public const string Identifier = $"{nameof(CinematicsSystem)}:{nameof(UICinematics)}";

	public override void OnWorldLoad() {
		base.OnWorldLoad();

		UISystem.TryEnableOrRegister(Identifier, "Vanilla: Mouse Text", new UICinematics());
	}
}
