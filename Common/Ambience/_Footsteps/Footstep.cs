using Newtonsoft.Json;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public sealed class Footstep : IFootstep
{
    [JsonRequired]
    public FootstepSoundData SoundData { get; set; }
    
    [JsonRequired]
    public string Material { get; set; }
}
