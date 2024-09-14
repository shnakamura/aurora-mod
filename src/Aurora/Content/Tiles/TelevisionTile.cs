using Aurora.Assets.Textures.Items.Miscellaneous;
using Terraria.DataStructures;
using Terraria.ObjectData;

namespace Aurora.Content.Tiles;

public class TelevisionTile : ModTile
{
	public override void SetStaticDefaults() {
		base.SetStaticDefaults();

		Main.tileFrameImportant[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLavaDeath[Type] = true;
		Main.tileSolidTop[Type] = true;
		
		TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);

		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(
			ModContent.GetInstance<TelevisionTileEntity>().Hook_AfterPlacement, 
			-1, 
			0, 
			true
		);

		TileObjectData.newTile.Origin = new Point16(2);
		
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

	public override bool RightClick(int i, int j) {
		var tile = Framing.GetTileSafely(i, j);
		
		var index = ModContent.GetInstance<TelevisionTileEntity>().Find(i, j);

		if (index == -1) {
			return false;
		}
		
		var entity = (TelevisionTileEntity)TileEntity.ByID[index];

		entity.Toggle();

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
		
		ModContent.GetInstance<TelevisionTileEntity>().Kill(i, j);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
		base.PostDraw(i, j, spriteBatch);

		var tile = Framing.GetTileSafely(i, j);

		if (tile.TileFrameX != 18 * 3 || tile.TileFrameY != 18 * 3) {
			return;
		}

		var index = ModContent.GetInstance<TelevisionTileEntity>().Find(i, j);

		if (index == -1) {
			return;
		}

		var entity = (TelevisionTileEntity)TileEntity.ByID[index];
		
		entity.Draw(i, j, spriteBatch);
	}
}
