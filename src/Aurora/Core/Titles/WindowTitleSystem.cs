using MonoMod.Cil;
using Terraria.Localization;

namespace Aurora.Core.Window;

[Autoload(Side = ModSide.Client)]
public sealed class WindowTitleSystem : ModSystem
{
	/// <summary>
	///		The total amount of custom window titles.
	/// </summary>
	public const int Count = 11;

	public override void Load() {
		base.Load();

		// TODO: Re-implement the patch since it became obsolete in the latest game update.
		// IL_Main.DrawMenu += DrawMenuPatch;
	}

	public override void PostSetupContent() {
		base.PostSetupContent();

		ChangeTitle();
	}

	private static void ChangeTitle() {
		var index = Main.rand.Next(Count);

		Main.changeTheTitle = false;
		Main.instance.Window.Title = Language.GetTextValue($"Mods.{nameof(Aurora)}.GameTitle.{index}");
	}
}
