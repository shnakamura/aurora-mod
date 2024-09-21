using Aurora.Core.EC;

namespace Aurora.Common.Movement;

public sealed class Velocity(float x = 0f, float y = 0f) : Component
{
	public float X = x;
	public float Y = y;
}
