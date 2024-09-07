using Newtonsoft.Json;

namespace Aurora.Common;

public sealed class Footstep : IFootstep
{
    [JsonRequired]
    public FootstepSoundData SoundData { get; set; }

    [JsonRequired]
    public string Material { get; set; }
}
