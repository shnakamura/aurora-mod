using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public sealed class AmbienceTrack : IAmbienceTrack
{
    private float volume = 0f;

    /// <summary>
    ///     The volume of this track. Ranges from <c>0f</c> to <c>1f</c>.
    /// </summary>
    [JsonIgnore]
    public float Volume {
        get => volume;
        set => volume = MathHelper.Clamp(value, 0f, 1f);
    }

    /// <summary>
    ///     The sound slot of this track.
    /// </summary>
    [JsonIgnore]
    public SlotId Slot { get; set; } = SlotId.Invalid;

    /// <summary>
    ///     The volume step-in amount used by this track to perform fade-ins.
    /// </summary>
    public float StepIn { get; } = 0.05f;

    /// <summary>
    ///     The volume step-out amount used by this track to perform fade-outs.
    /// </summary>
    public float StepOut { get; } = 0.05f;
    
    /// <summary>
    ///     The sound style of this track.
    /// </summary>
    [JsonRequired]
    public SoundStyle Sound { get; } = default;

    /// <summary>
    ///     The flags used by this track to indicate whether it should be active or not.
    /// </summary>
    [JsonRequired]
    public string[] Flags { get; } = Array.Empty<string>();

    public AmbienceTrack() { }
}
