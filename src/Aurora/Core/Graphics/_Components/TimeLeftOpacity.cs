using Aurora.Common.Behavior;
using Aurora.Core.EC;

namespace Aurora.Core.Graphics;

public sealed class OpacityTimeLeft : Component
{
	public override void Update() {
		base.Update();

		if (!Entity.Has<Duration>() || !Entity.Has<PixellatedTextureRenderer>()) {
			return;
		}

		var timeLeft = Entity.Get<Duration>();
		var renderer = Entity.Get<PixellatedTextureRenderer>();

		renderer.Info.Opacity = timeLeft.Progress;
	}
}
