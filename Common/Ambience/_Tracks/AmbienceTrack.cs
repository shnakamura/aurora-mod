using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public sealed class AmbienceTrack : IAmbienceTrack
{
    private float volume = 0f;
    
    [JsonIgnore]
    public float Volume {
        get => volume;
        set => volume = MathHelper.Clamp(value, 0f, 1f);
    }
    
    [JsonIgnore]
    public SlotId Slot { get; set; } = SlotId.Invalid;
    
    public float StepIn { get; set; } = 0.05f;
    
    public float StepOut { get; set; } = 0.05f;

    [JsonRequired]
    public SoundStyle Sound { get; set; }

    [JsonRequired]
    public string[] Flags { get; set; } = Array.Empty<string>();

    public AmbienceTrack() { }
}