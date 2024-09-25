using Aurora.Core.EC;

namespace Aurora.Common.Behavior;

public sealed class Duration(int timeLeft) : Component
{
	public float Progress => 1f - TimeLeft / (float)TimeLeftMax;

	/// <summary>
	///		The remaining time of the entity attached to this component in ticks.
	/// </summary>
	public int TimeLeft = timeLeft;

	/// <summary>
	///		The max time of the entity attached to this component in ticks.
	/// </summary>
	public readonly int TimeLeftMax = timeLeft;

	public override void Update() {
		base.Update();

		TimeLeft--;

		if (TimeLeft > 0) {
			return;
		}

		EntitySystem.Remove(Entity.Id);
	}
}
