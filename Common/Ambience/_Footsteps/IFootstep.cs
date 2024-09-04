using Terraria.Audio;

namespace Aurora.Common.Ambience;

public interface IFootstep
{
    SoundStyle Sound { get; }

    string[] Materials { get; }
}
