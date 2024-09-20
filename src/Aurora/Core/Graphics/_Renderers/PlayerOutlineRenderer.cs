using Aurora.Core.Graphics;
using Aurora.Utilities;
using ReLogic.Content;

namespace Aurora.Content.Items.Miscellaneous;

[Autoload(Side = ModSide.Client)]
public sealed class PlayerOutlineRenderer : ModSystem
{
	private static readonly Color OutlineColor = new(219, 26, 26);

	public override void Load() {
		base.Load();

		On_Main.DrawInfernoRings += DrawInfernoRingsHook;
	}

	public override void PostUpdateWorld() {
		base.PostUpdateWorld();
	}

	private void DrawInfernoRingsHook(On_Main.orig_DrawInfernoRings orig, Main self) {
		RenderOutline();

		orig(self);
	}

	private void RenderOutline() {
		var spriteBatch = Main.spriteBatch;

		var snapshot = spriteBatch.Capture();

		var effect = Mod.Assets.Request<Effect>("Assets/Effects/Outline", AssetRequestMode.ImmediateLoad).Value;

		spriteBatch.End();
		spriteBatch.Begin(
			snapshot.SpriteSortMode,
			BlendState.NonPremultiplied,
			SamplerState.PointClamp,
			snapshot.DepthStencilState,
			snapshot.RasterizerState,
			effect,
			snapshot.TransformMatrix
		);

		effect.Parameters["uSize"].SetValue(2f);
		effect.Parameters["uOpacity"].SetValue(1f);
		effect.Parameters["uColor"].SetValue(OutlineColor.ToVector3());
		effect.Parameters["uImageSize0"].SetValue(PlayerRenderSystem.Target.Size());

		spriteBatch.End();
		spriteBatch.Begin(in snapshot);
	}
}
