using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Aurora.Core.Configuration;

public sealed class ClientConfiguration : ModConfig
{
	public static ClientConfiguration Instance => ModContent.GetInstance<ClientConfiguration>();
	
	public override ConfigScope Mode { get; } = ConfigScope.ClientSide;

	/// <summary>
	///		Whether ambience tracks are enabled or not.
	/// </summary>
	[Header("Ambience")]
	[DefaultValue(true)]
	public bool EnableAmbience { get; set; } = true;
	
	/// <summary>
	///		Whether ambience footsteps are enabled or not.
	/// </summary>
	[DefaultValue(true)]
	public bool EnableFootsteps { get; set; } = true;

	/// <summary>
	///		Whether the low pass filter is enabled or not.
	/// </summary>
	[Header("Audio")]
	[DefaultValue(true)]
	public bool EnableLowPassFilter { get; set; } = true;

	/// <summary>
	///		Whether the camera smoothing modifier is enabled or not.
	/// </summary>
	[Header("Camera")]
	[DefaultValue(true)]
	public bool EnableSmoothing { get; set; } = true;
}
