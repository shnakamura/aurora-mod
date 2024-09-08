using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public interface IAmbienceTrack : ILoadable
{
	/// <summary>
	///		The file extension associated with this data type.
	/// </summary>
	public const string Extension = ".ambience.track";
	
    AmbienceTrackSoundData SoundData { get; set; }

    string[] Signals { get; set; }

    float Volume { get; set; }

    float StepIn { get; set; }

    float StepOut { get; set; }

    SlotId Slot { get; set; }
    
    void ILoadable.Load(Mod mod) { }
	
    void ILoadable.Unload() { }
}
