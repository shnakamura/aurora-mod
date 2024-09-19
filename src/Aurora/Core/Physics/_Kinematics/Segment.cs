namespace Aurora.Common.Physics;

public sealed class Segment
{
	public Vector2 Start;
	public Vector2 End;

	public float Length;
	public float Rotation;

	public Segment(Vector2 start, float length, float rotation = 0f) {
		Start = start;

		Length = length;
		Rotation = rotation;

		End = Start + Rotation.ToRotationVector2() * Length;
	}

	public void Follow(Vector2 position) {
		End = Start + Rotation.ToRotationVector2() * Length;

		Rotation = Start.AngleTo(position);

		var direction = position - Start;

		direction = direction.SafeNormalize(Vector2.Zero) * Length;
		direction *= -1;

		Start = position + direction;
	}

	public void Draw() {
		Utils.DrawLine(Main.spriteBatch, Start, End, Color.White, Color.White, 10f);
	}
}
