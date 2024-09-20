using Aurora.Content.Items.Miscellaneous;
using Aurora.Core.UI;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ObjectData;
using Terraria.UI;

namespace Aurora.Content.Tiles;

public class TelevisionTile : ModTile
{
	public override void SetStaticDefaults() {
		base.SetStaticDefaults();

		Main.tileFrameImportant[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLavaDeath[Type] = true;

		TileID.Sets.HasOutlines[Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);

		TileObjectData.newTile.Origin = Point16.Zero;

		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16];

		TileObjectData.addTile(Type);

		MineResist = 1f;

		DustType = DustID.Glass;
		HitSound = SoundID.Shatter;
	}

	public override void NumDust(int i, int j, bool fail, ref int num) {
		base.NumDust(i, j, fail, ref num);

		num = fail ? 1 : 3;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
		return true;
	}

	public override void NearbyEffects(int i, int j, bool closer) {
		base.NearbyEffects(i, j, closer);

		if (closer) {
			return;
		}

	}

	public override bool RightClick(int i, int j) {
		UISystem.TryToggleOrRegister(UITelevision.Identifier, "Vanilla: Mouse Text", new UITelevision());

		return true;
	}

	public override void MouseOver(int i, int j) {
		var player = Main.LocalPlayer;

		player.noThrow = 2;

		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = ModContent.ItemType<TelevisionItem>();
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		base.KillMultiTile(i, j, frameX, frameY);

		UISystem.TryDisable(UITelevision.Identifier);
	}
}
