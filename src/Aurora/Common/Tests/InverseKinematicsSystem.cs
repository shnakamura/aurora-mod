using Aurora.Core.Graphics;
using Aurora.Core.Physics;
using ReLogic.Content;
using Terraria.GameContent;

namespace Aurora.Common.Tests;

public sealed class InverseKinematicsSystem : ModSystem
{
	public override void Load() {
		base.Load();

		On_Main.DrawInfernoRings += On_MainOnDrawInfernoRings;
	}

	private void On_MainOnDrawInfernoRings(On_Main.orig_DrawInfernoRings orig, Main self) {
		orig(self);

		PixellatedRenderer.Queue(
			static () => {
				Main.spriteBatch.Draw(
					ModContent.Request<Texture2D>("Aurora/Assets/Textures/Items/Scarlet/MOGUS").Value,
					Main.LocalPlayer.Center - Main.screenPosition,
					Color.White
				);
			}
		);
	}
}
