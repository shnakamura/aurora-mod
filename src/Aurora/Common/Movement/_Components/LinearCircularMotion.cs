using Aurora.Common.Behavior;
using Aurora.Core.EC;
using Aurora.Core.Physics;

namespace Aurora.Common.Movement;

public sealed class LinearCircularMotion(float frequency, float length, int direction) : Component
{
	/// <summary>
	///		The frequency of this component's movement.
	/// </summary>
	public float Frequency = frequency;

	/// <summary>
	///		The length of this component's movement in pixels.
	/// </summary>
	public float Length = length;

	/// <summary>
	///		The direction of this component's movement.
	/// </summary>
	public int Direction = direction;

	private float timer;

	private Vector2 previousOffset;

	public override void Update() {
		base.Update();

		if (!Entity.Has<Transform>() || !Entity.Has<Velocity>()) {
			return;
		}

		var transform = Entity.Get<Transform>();
		var velocity = Entity.Get<Velocity>();

		timer += Frequency * Direction;

		var offset = timer.ToRotationVector2() * Length;

		velocity.X += offset.X - previousOffset.X;
		velocity.Y += offset.Y - previousOffset.Y;

		previousOffset = offset;
	}
}
