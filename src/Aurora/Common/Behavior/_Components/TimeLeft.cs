using Aurora.Core.EC;

namespace Aurora.Common.Behavior;

public sealed class TimeLeft(int duration) : Component
{
	public float Progress => Duration / (float)DurationMax;

	/// <summary>
	///		The remaining time of the entity attached to this component in ticks.
	/// </summary>
	public int Duration = duration;

	/// <summary>
	///		The max time of the entity attached to this component in ticks.
	/// </summary>
	public readonly int DurationMax = duration;

	private int timer;

	public override void Update() {
		base.Update();

		Duration--;

		if (Duration > 0) {
			return;
		}

		EntitySystem.Remove(Entity.Id);
	}
}
