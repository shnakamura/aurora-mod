using Newtonsoft.Json;

namespace Aurora.Common.Ambience;

public struct FootstepSoundData
{
    [JsonRequired]
    public string SoundPath { get; set; }

    [JsonRequired]
    public int Variants { get; set; }
}
