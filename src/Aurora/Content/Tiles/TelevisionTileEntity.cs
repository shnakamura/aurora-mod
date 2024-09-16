using System.IO;
using Microsoft.Xna.Framework.Media;
using ReLogic.Content;

namespace Aurora.Content.Tiles;

public class TelevisionTileEntity : ModTileEntity
{
	public readonly TelevisionTileEntityData Data = new();
	
	public override bool IsTileValidForEntity(int x, int y) {
		var tile = Framing.GetTileSafely(x, y);

		var isValid = tile.HasTile && tile.TileType == ModContent.TileType<TelevisionTile>();
		var isFrame = tile.TileFrameX == 3 * 18 && tile.TileFrameY == 3 * 18;

		return isValid && isFrame;
	}

	public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate) {
		if (Main.netMode == NetmodeID.MultiplayerClient) {
			NetMessage.SendTileSquare(Main.myPlayer, i + 3, j + 3, 4, 4);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i + 3, j + 3, Type);
			return -1;
		}
		
		return Place(i + 3, j + 3);
	}
	
	public override void Update() {
		base.Update();

		Data.Player.Volume = Main.soundVolume;
	}

	public void Draw(int i, int j, SpriteBatch spriteBatch) {
		if (Data.Player.State != MediaState.Playing) {
			return;
		}
		
		var offset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
		
		// Hardcoded values to match the tile's designed area for drawing the video.
		var rectangle = new Rectangle(
			(int)(i * 16f - Main.screenPosition.X + offset.X - 44f), 
			(int)(j * 16f - Main.screenPosition.Y + offset.Y - 28f), 
			40, 
			34
		);
		
		spriteBatch.Draw(Data.Player.GetTexture(), rectangle, Color.White);
	}
}
