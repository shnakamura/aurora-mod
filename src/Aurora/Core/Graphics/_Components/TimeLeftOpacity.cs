using Aurora.Common.Behavior;
using Aurora.Core.EC;

namespace Aurora.Core.Graphics;

public sealed class OpacityTimeLeft : Component
{
	public override void Update() {
		base.Update();

		if (!Entity.Has<TimeLeft>() || !Entity.Has<PixellatedTextureRenderer>()) {
			return;
		}

		var timeLeft = Entity.Get<TimeLeft>();
		var renderer = Entity.Get<PixellatedTextureRenderer>();

		renderer.Info.Opacity = timeLeft.Progress;
	}
}
