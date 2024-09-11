using Aurora.Core.Graphics;
using Aurora.Utilities;
using JetBrains.Annotations;
using ReLogic.Content;

namespace Aurora.Content.Items.Wildlife;

[Autoload(Side = ModSide.Client)]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class WildWarriorPlayerRenderer : ModSystem
{
	private static readonly Color OutlineColor = new(247, 226, 70);

	private static float opacity;
	
	public override void Load() {
		base.Load();
		
		On_Main.DrawInfernoRings += DrawInfernoRingsHook;
	}

	private void DrawInfernoRingsHook(On_Main.orig_DrawInfernoRings orig, Main self) {
		RenderOutline();
		
		orig(self);
	}

	private void RenderOutline() {
		var player = Main.LocalPlayer;

		if (!player.TryGetModPlayer(out WildWarriorPlayer modPlayer) || !modPlayer.Enabled) {
			return;
		}
		
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

		Main.NewText(modPlayer.DodgeCooldown);
		opacity = MathHelper.SmoothStep(opacity, modPlayer.CanDodge ? 1f : 0f, 0.3f);
		
		effect.Parameters["uSize"].SetValue(2f);
		effect.Parameters["uOpacity"].SetValue(opacity);
		effect.Parameters["uColor"].SetValue(OutlineColor.ToVector3());
		effect.Parameters["uImageSize0"].SetValue(PlayerRenderSystem.Target.Size());
		
		spriteBatch.Draw(PlayerRenderSystem.Target, Vector2.Zero, Color.White);
		
		spriteBatch.End();
		spriteBatch.Begin(in snapshot);
	}
}
