using Terraria.DataStructures;

namespace Aurora.Content.Items.Miscellaneous;

/// <summary>
///		Handles the behavior of increasing the player's gravity and fall speed if the player is holding
///		down with a <see cref="StoneBootsItem"/> equipped.
/// </summary>
public sealed class StoneBootsPlayer : ModPlayer
{
	/// <summary>
	///		Whether this player is wearing a <see cref="StoneBootsItem"/> and holding down or not.
	/// </summary>
	public bool Enabled;

	public override void ResetEffects() {
		base.ResetEffects();

		Enabled = false;
	}

	public override void PostUpdateEquips() {
		base.PostUpdateEquips();

		if (!Enabled) {
			return;
		}

		Player.maxFallSpeed *= 2f;
		Player.gravity *= 2f;
	}

	public override void FrameEffects() {
		base.FrameEffects();

		if (!Enabled) {
			return;
		}

		Player.armorEffectDrawShadow = true;
		Player.armorEffectDrawOutlines = true;
	}
}
