namespace Aurora.Content.Items.Accessories;

public sealed class PowerMagnetPlayer : ModPlayer
{
	/// <summary>
	///		Whether this player's accessory is enabled or not.
	/// </summary>
	public bool Enabled { get; set; }

	/// <summary>
	///		Whether to display this player's accessory or not.
	/// </summary>
	public bool Display { get; set; }

	public override void ResetEffects() {
		base.ResetEffects();

		Enabled = false;
		Display = false;
	}
}
