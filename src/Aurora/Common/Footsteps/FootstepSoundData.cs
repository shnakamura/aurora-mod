using Newtonsoft.Json;

namespace Aurora.Common;

public struct FootstepSoundData
{
    [JsonRequired]
    public string SoundPath { get; set; }

    [JsonRequired]
    public int Variants { get; set; }
}
