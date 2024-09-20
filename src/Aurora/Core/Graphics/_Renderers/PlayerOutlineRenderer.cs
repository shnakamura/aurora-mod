using System.Collections.Generic;
using Aurora.Core.Graphics;
using Aurora.Utilities;
using ReLogic.Content;

namespace Aurora.Content.Items.Miscellaneous;

/// <summary>
///		Handles the registration and rendering of player outlines.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class PlayerOutlineRenderer : ModSystem
{
	public readonly struct OutlineData(Color color, float opacity = 1f, float thickness = 2f)
	{
		/// <summary>
		///		The color of the outline.
		/// </summary>
		public readonly Color Color = color;

		/// <summary>
		///		The opacity of the outline. Defaults to <c>1f</c>.
		/// </summary>
		public readonly float Opacity = opacity;

		/// <summary>
		///		The thickness of the outline. Defaults to <c>2f</c>.
		/// </summary>
		public readonly float Thickness = thickness;
	}

	private static readonly Asset<Effect> Shader = ModContent.Request<Effect>(
		$"{nameof(Aurora)}/Assets/Effects/Outline",
		AssetRequestMode.ImmediateLoad
	);

	private static readonly List<OutlineData> Data = [];

	public override void Load() {
		base.Load();

		On_Main.DrawInfernoRings += DrawInfernoRingsHook;
	}

	/// <summary>
	///		Adds a player outline to the rendering queue.
	/// </summary>
	/// <param name="color">The color of the outline.</param>
	/// <param name="opacity">The opacity of the outline.</param>
	/// <param name="thickness">The thickness of the outline in pixels.</param>
	public static void Draw(Color color, float opacity = 1f, float thickness = 2f) {
		Data.Add(new OutlineData(color, opacity, thickness));
	}

	private static void DrawInfernoRingsHook(On_Main.orig_DrawInfernoRings orig, Main self) {
		RenderData();

		orig(self);
	}

	private static void RenderData() {
		var spriteBatch = Main.spriteBatch;

		var snapshot = spriteBatch.Capture();

		var effect = Shader.Value;

		foreach (var data in Data) {
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

            effect.Parameters["uSize"].SetValue(data.Thickness);
            effect.Parameters["uOpacity"].SetValue(data.Opacity);
            effect.Parameters["uColor"].SetValue(data.Color.ToVector3());
            effect.Parameters["uImageSize0"].SetValue(PlayerRenderer.Target.Size());

            spriteBatch.End();
            spriteBatch.Begin(in snapshot);
		}

		Data.Clear();
	}
}
