using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.Localization;

namespace Aurora.Core.Titles;

/// <summary>
///     Handles the registration and updating of custom window titles.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class WindowTitleSystem : ModSystem
{
	/// <summary>
	///     The total amount of custom window titles.
	/// </summary>
	public const int Count = 11;

	public override void Load() {
		base.Load();

		IL_Main.DrawMenu += DrawMenuEdit;
	}

	public override void PostSetupContent() {
		base.PostSetupContent();

		ChangeTitle();
	}

	private static void ChangeTitle() {
		var index = Main.rand.Next(Count);

		Main.instance.Window.Title = Language.GetTextValue($"Mods.{nameof(Aurora)}.GameTitle.{index}");
	}

	private static void DrawMenuEdit(ILContext il) {
		try {
			var c = new ILCursor(il);

			if (!c.TryGotoNext(MoveType.After, static i => i.MatchLdcI4(1), static i => i.MatchStsfld<Main>(nameof(Main.changeTheTitle)))) {
				return;
			}

			c.EmitDelegate(ChangeTitle);

			if (!c.TryGotoNext(static i => i.MatchLdcI4(1), static i => i.MatchStsfld<Main>(nameof(Main.changeTheTitle)))) {
				return;
			}

			c.EmitDelegate(ChangeTitle);
		}
		catch (Exception) {
			MonoModHooks.DumpIL(Aurora.Instance, il);
		}
	}
}
