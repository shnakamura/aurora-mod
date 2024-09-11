using Aurora.Core.Graphics;
using Aurora.Utilities;
using JetBrains.Annotations;
using ReLogic.Content;

namespace Aurora.Content.Items.Wildlife;

[Autoload(Side = ModSide.Client)]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class WildWarriorPlayerRenderer : ModSystem
{
	public override void Load() {
		base.Load();
		
		On_Main.DrawInfernoRings += DrawInfernoRingsHook;
	}

	private void DrawInfernoRingsHook(On_Main.orig_DrawInfernoRings orig, Main self) {
		var spriteBatch = Main.spriteBatch;

		var snapshot = spriteBatch.Capture();
		
		var effect = Mod.Assets.Request<Effect>("Assets/Effects/Outline", AssetRequestMode.ImmediateLoad).Value;
		
		spriteBatch.End();
		spriteBatch.Begin(
			snapshot.SpriteSortMode,
			snapshot.BlendState,
			SamplerState.PointClamp, 
			snapshot.DepthStencilState,
			snapshot.RasterizerState,
			effect,
			snapshot.TransformMatrix
		);

		effect.Parameters["uSize"].SetValue(2f);
		effect.Parameters["uColor"].SetValue(Color.White.ToVector3());
		effect.Parameters["uImageSize0"].SetValue(PlayerRenderSystem.Target.Size());
		
		spriteBatch.Draw(PlayerRenderSystem.Target, Vector2.Zero, Color.White);
		
		spriteBatch.End();
		spriteBatch.Begin(in snapshot);
		
		orig(self);
	}
}
