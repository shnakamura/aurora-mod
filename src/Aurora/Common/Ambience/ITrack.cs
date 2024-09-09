using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public interface ITrack : ILoadable
{
	SoundStyle Sound { get; init; }
	
    string[] Signals { get; init; }

    float StepIn { get; init; }

    float StepOut { get; init; }
    
    float Volume { get; set; }

    SlotId Slot { get; set; }
    
    void ILoadable.Load(Mod mod) { }
	
    void ILoadable.Unload() { }
}
