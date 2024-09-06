using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public sealed class AmbienceTrack : IAmbienceTrack
{
    [JsonRequired]
    public AmbienceTrackSoundData SoundData { get; set; }
    
    [JsonRequired]
    public string[] Signals { get; set; }
    
    private float volume;

    [JsonIgnore]
    public float Volume {
        get => volume;
        set => volume = MathHelper.Clamp(value, 0f, 1f);
    }

    [JsonIgnore]
    public SlotId Slot { get; set; } = SlotId.Invalid;

    public float StepIn { get; set; } = 0.05f;

    public float StepOut { get; set; } = 0.05f;
}
