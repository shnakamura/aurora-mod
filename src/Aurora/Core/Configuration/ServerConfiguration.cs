using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Aurora.Core.Configuration;

public sealed class ServerConfiguration : ModConfig
{
	public static ServerConfiguration Instance => ModContent.GetInstance<ServerConfiguration>();

	public override ConfigScope Mode { get; } = ConfigScope.ServerSide;

	[Header("Items")]
	[ReloadRequired]
	[DefaultValue(true)]
	public bool EnableRecipes { get; set; } = true;
}
