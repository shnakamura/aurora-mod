using Terraria.Audio;

namespace Aurora.Common.Ambience;

public interface IFootstep : ILoadable
{
    SoundStyle Sound { get; init; }
	
    string Material { get; init; }

	void ILoadable.Load(Mod mod) { }

	void ILoadable.Unload() { }
}
