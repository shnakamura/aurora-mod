using ReLogic.Content;
using Terraria.DataStructures;

namespace Aurora.Content.Items.Accessories;

public sealed class PowerMagnetItemLayer : PlayerDrawLayer
{
	private static readonly Asset<Texture2D> DisplayTexture =
		ModContent.Request<Texture2D>($"{nameof(Aurora)}/Content/Items/Accessories/PowerMagnetItem_Display");
		
	private static readonly Asset<Texture2D> DisplayOutlineTexture =
		ModContent.Request<Texture2D>($"{nameof(Aurora)}/Content/Items/Accessories/PowerMagnetItem_Display_Outline");
		
	public override Position GetDefaultPosition() {
		return new AfterParent(PlayerDrawLayers.OffhandAcc);
	}

	protected override void Draw(ref PlayerDrawSet drawInfo) {
		var player = drawInfo.drawPlayer;

		if (!player.TryGetModPlayer(out PowerMagnetItemPlayer modPlayer) || !modPlayer.Display) {
			return;
		}
			
		var position = player.Center - new Vector2(0f, 72f);

		var sineOffset = new Vector2(0f, MathF.Sin(Main.GameUpdateCount * 0.05f) * 4f);
		var drawPosition = position - Main.screenPosition + new Vector2(0f, player.gfxOffY) + sineOffset;

		drawInfo.DrawDataCache.Add(new DrawData(
			DisplayTexture.Value,
			drawPosition, 
			null, 
			Lighting.GetColor(position.ToTileCoordinates()), 
			player.fullRotation,
			DisplayTexture.Size() / 2f,
			1f, 
			SpriteEffects.None
		));
			
		drawInfo.DrawDataCache.Add(new DrawData(
			DisplayOutlineTexture.Value,
			drawPosition, 
			null, 
			Color.White, 
			player.fullRotation,
			DisplayOutlineTexture.Size() / 2f,
			1f, 
			SpriteEffects.None
		));
	}
}
