using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Aurora.Common.Configuration;

public sealed class ClientConfiguration : ModConfig
{
	public static ClientConfiguration Instance => ModContent.GetInstance<ClientConfiguration>();
	
	public override ConfigScope Mode { get; } = ConfigScope.ClientSide;

	/// <summary>
	///		Whether ambience tracks are enabled or not.
	/// </summary>
	[DefaultValue(true)]
	public bool EnableAmbience { get; set; } = true;
	
	/// <summary>
	///		Whether footsteps are enabled or not.
	/// </summary>
	[DefaultValue(true)]
	public bool EnableFootsteps { get; set; } = true;
	
	/// <summary>
	///		Whether water muffling sound effects are enabled or not.
	/// </summary>
	[Header("Audio")]
	[DefaultValue(true)]
	public bool EnableMuffling { get; set; } = true;
}
