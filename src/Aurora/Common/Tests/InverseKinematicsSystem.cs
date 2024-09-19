namespace Aurora.Common.Tests;

public sealed class InverseKinematicsSystem : ModSystem
{
	public sealed class Segment
	{
		public Vector2 Start;
		public Vector2 End;

		public float Angle;
		public float Length;

		public Segment(Vector2 start, float angle, float length)
		{
			Start = start;
			Angle = angle;
			Length = length;
			CalculateEnd();
		}

		public void CalculateEnd()
		{
			var direction = Angle.ToRotationVector2() * Length;
			End = Start + direction;
		}

		public void Follow(Vector2 target)
		{
			var toTarget = target - Start;
			Angle = (float)Math.Atan2(toTarget.Y, toTarget.X); // Calculate angle towards the target
			CalculateEnd();
		}

		public void UpdateStart(Vector2 newStart)
		{
			Start = newStart;
			CalculateEnd();
		}

		public void Draw()
		{
			Utils.DrawLine(
				Main.spriteBatch,
				Start,
				End,
				Color.White,
				Color.White,
				4f
			);
		}
	}

	private Segment upperArm;
	private Segment forearm;

	public override void Load()
	{
		base.Load();
		On_Main.DrawInfernoRings += On_MainOnDrawInfernoRings;



	}

	public override void PostUpdateDusts() {
		base.PostUpdateDusts();

		Vector2 startPos = new Vector2(0f, 100f);

		float upperArmLength = 50f;
		float forearmLength = 40f;

		upperArm ??= new Segment(Main.LocalPlayer.Center - startPos, 0f, upperArmLength);
		forearm ??= new Segment(Main.LocalPlayer.Center - startPos, 0f, forearmLength);
	}

	private void On_MainOnDrawInfernoRings(On_Main.orig_DrawInfernoRings orig, Main self)
	{
		orig(self);

		if (forearm is null || upperArm is null) {
			return;
		}

		// Get the local player
		Player player = Main.LocalPlayer;

		// Set the player's center as the starting position of the upper arm
		Vector2 playerCenter = player.Center;

		// Get the cursor position as the target
		Vector2 cursorPosition = Main.MouseWorld;

		// Update the arm segments to follow the cursor
		upperArm.Follow(cursorPosition);
		forearm.Follow(upperArm.Start); // Upper arm should follow forearm's start

		// Update the starting position of the forearm based on the upper arm
		upperArm.UpdateStart(playerCenter);
		forearm.UpdateStart(upperArm.End);

		// Draw the arm segments
		upperArm.Draw();
		forearm.Draw();
	}
}
