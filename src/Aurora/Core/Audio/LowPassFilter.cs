using System.Reflection;
using Microsoft.Xna.Framework.Audio;

namespace Aurora.Core.Audio;

public sealed class LowPassFilter : IAudioFilter
{
    void ILoadable.Load(Mod mod) { }

    void ILoadable.Unload() { }

    void IAudioFilter.Apply(SoundEffectInstance instance, in AudioParameters parameters) {
        var intensity = parameters.LowPass;

        if (intensity <= 0f || instance?.IsDisposed == true) {
            return;
        }

        instance.INTERNAL_applyLowPassFilter(1f - intensity);
    }
}
