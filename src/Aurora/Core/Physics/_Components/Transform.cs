using Aurora.Common.Movement;
using Aurora.Core.EC;

namespace Aurora.Core.Physics;

public sealed class Transform(Vector2 position, Vector2? scale = null, float rotation = 0f) : Component
{
	/// <summary>
	///		The position of this transform in world coordinates.
	/// </summary>
	public Vector2 Position = position;

	/// <summary>
	///		The scale of this transform.
	/// </summary>
	public Vector2 Scale = scale ?? Vector2.One;

	/// <summary>
	///		The rotation of this transform in radians.
	/// </summary>
	public float Rotation = rotation;

	public override void Update() {
		base.Update();

		if (!Entity.Has<Velocity>()) {
			return;
		}

		var velocity = Entity.Get<Velocity>();

		Position.X += velocity.X;
		Position.Y += velocity.Y;
	}

	public override string ToString() {
		return $"Position: {position} @ Scale: {scale} @ Rotation: {rotation}";
	}
}
