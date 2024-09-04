using Newtonsoft.Json;

namespace Aurora.Common.Ambience;

public struct FootstepSoundData : IFootstepSoundData
{
    [JsonRequired]
    public string SoundPath { get; set; }
    
    [JsonRequired]
    public int Variants { get; set; }
}
