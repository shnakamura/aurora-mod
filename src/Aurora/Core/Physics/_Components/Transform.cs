using Aurora.Core.EC;

namespace Aurora.Core.Physics;

public sealed class Transform(Vector2 position, Vector2? scale = null, float rotation = 0f) : Component
{
	public Vector2 Position = position;

	public Vector2 Scale = scale ?? Vector2.One;

	public float Rotation = rotation;
}
