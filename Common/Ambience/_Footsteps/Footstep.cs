using Newtonsoft.Json;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public sealed class Footstep : IFootstep
{
    [JsonRequired]
    public SoundStyle Sound { get; set; }

    [JsonRequired]
    public string[] Materials { get; set; }
}
