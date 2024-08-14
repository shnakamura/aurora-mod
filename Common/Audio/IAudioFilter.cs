using Microsoft.Xna.Framework.Audio;

namespace Aurora.Common.Audio;

/// <summary>
///     An implementation of an audio filter used by <see cref="AudioManager" />.
/// </summary>
[Autoload(Side = ModSide.Client)]
public interface IAudioFilter : ILoadable
{
    /// <summary>
    ///     Applies audio modifiers to audio parameters from a given sound instance.
    /// </summary>
    /// <param name="instance">The sound instance.</param>
    /// <param name="parameters">The sound parameters.</param>
    void Apply(SoundEffectInstance instance, in AudioParameters parameters);
}
