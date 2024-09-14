using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Aurora.Common.Cinematics;

public sealed class UICinematics : UIState
{
	private UIImage topBox;
	private UIImage bottomBox;
	
	private bool enabled;
	
	private float progress;

	public override void OnInitialize() {
		base.OnInitialize();

		topBox = new UIImage(TextureAssets.MagicPixel) {
			Width = { Precent = 1f },
			ScaleToFit = true,
			Color = Color.Black,
			OverrideSamplerState = SamplerState.PointClamp
		};
		
		Append(topBox);
		
		bottomBox = new UIImage(TextureAssets.MagicPixel) {
			Width = { Precent = 1f },
			VAlign = 1f,
			ScaleToFit = true,
			Color = Color.Black,
			OverrideSamplerState = SamplerState.PointClamp
		};
		
		Append(bottomBox);
	}

	public override void OnActivate() {
		base.OnActivate();

		enabled = true;
	}

	public override void OnDeactivate() {
		base.OnDeactivate();

		enabled = false;
	}

	public override void Update(GameTime gameTime) {
		base.Update(gameTime);
		
		topBox.Height.Set(200f * progress, 0f);
		bottomBox.Height.Set(200f * progress, 0f);

		progress = MathHelper.SmoothStep(progress, enabled ? 1f : 0f, 0.2f);
	}
}
