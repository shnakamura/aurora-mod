using Newtonsoft.Json;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public sealed class Footstep
{
    [JsonRequired]
    public SoundStyle Sound;

    [JsonRequired]
    public string[] Materials;
}
