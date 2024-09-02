using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public interface IAmbienceTrack
{
    float StepIn { get; }
    
    float StepOut { get; }
    
    float Volume { get; }

    SlotId Slot { get; }

    SoundStyle Sound { get; }

    string[] Flags { get; }
}
