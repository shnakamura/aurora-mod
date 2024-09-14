using Terraria.ModLoader.IO;

namespace Aurora.Content.Items.Wildlife;

public sealed class WildWarriorPlayer : ModPlayer
{
	/// <summary>
	///		The maximum duration in ticks of this player's armor set dodge cooldown.
	/// </summary>
	public const int DodgeCooldownMax = 30 * 60;

	/// <summary>
	///		The cooldown of this player's armor set dodge.
	/// </summary>
	public int DodgeCooldown {
		get => _dodgeCooldown;
		set => _dodgeCooldown = MathHelper.Clamp(value, 0, 5 * 60);
	}

	private int _dodgeCooldown;

	/// <summary>
	///		Whether this player's armor set can dodge or not.
	/// </summary>
	public bool CanDodge => DodgeCooldown == 0;

	/// <summary>
	///		Whether this player's armor set is enabled or not.
	/// </summary>
	public bool Enabled { get; set; }

	public override void PostUpdateMiscEffects() {
		base.PostUpdateMiscEffects();

		if (Enabled) {
			DodgeCooldown--;
		}
		else {
			DodgeCooldown = DodgeCooldownMax;
		}
	}

	public override bool ConsumableDodge(Player.HurtInfo info) {
		if (Enabled && CanDodge) {
			ApplyDodge();
			return true;
		}
		
		return base.ConsumableDodge(info);
	}
	
	public override void SaveData(TagCompound tag) {
		base.SaveData(tag);

		tag["dodgeCooldown"] = DodgeCooldown;
	}

	public override void LoadData(TagCompound tag) {
		base.LoadData(tag);

		DodgeCooldown = tag.GetInt("dodgeCooldown");
	}

	private void ApplyDodge() {
		Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
		
		DodgeCooldown = DodgeCooldownMax;
	}
}
