using Aurora.Core.EC;
using Aurora.Core.Physics;

namespace Aurora.Core.Graphics;

public sealed class PixellatedTextureRenderer(SpriteBatchRenderInfo info) : Component
{
	public SpriteBatchRenderInfo Info = info;

	public override void Render() {
		base.Render();

		if (!Entity.Has<Transform>()) {
			return;
		}

		var transform = Entity.Get<Transform>();

		PixellatedRendererSystem.Queue(
			() => {
				if (Info.DestinationRectangle.HasValue) {
					Main.spriteBatch.Draw(
						Info.Texture.Value,
						Info.DestinationRectangle.Value,
						Info.SourceRectangle,
						Info.Color,
						transform.Rotation,
						Info.Origin,
						Info.Effects,
						0f
					);
				}
				else {
					Main.spriteBatch.Draw(
						Info.Texture.Value,
						transform.Position - Main.screenPosition,
						Info.SourceRectangle,
						Info.Color,
						transform.Rotation,
						Info.Origin,
						transform.Scale,
						Info.Effects,
						0f
					);
				}
			}
		);
	}
}
