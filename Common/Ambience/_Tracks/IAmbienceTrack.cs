using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public interface IAmbienceTrack
{
    AmbienceTrackSoundData SoundData { get; set; }
    
    string[] Signals { get; set; }
    
    /// <summary>
    ///     The volume of this track. Ranges from <c>0f</c> to <c>1f</c>.
    /// </summary>
    float Volume { get; set; }

    /// <summary>
    ///     The volume step-in amount used by this track to perform fade-ins.
    /// </summary>
    float StepIn { get; set; }

    /// <summary>
    ///     The volume step-out amount used by this track to perform fade-outs.
    /// </summary>
    float StepOut { get; set; }

    /// <summary>
    ///     The sound slot of this track.
    /// </summary>
    SlotId Slot { get; set; }
}
