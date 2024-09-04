using Terraria.Audio;

namespace Aurora.Common.Ambience;

public interface IFootstep
{
    FootstepSoundData SoundData { get; set; }

    string Material { get; set; }
}
