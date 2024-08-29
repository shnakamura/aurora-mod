using System.Reflection;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;

namespace Aurora.Common.Audio;

/// <summary>
///     Provides an implementation of an audio low pass filter.
/// </summary>
public sealed class LowPassFilter : IAudioFilter
{
    private static readonly Action<SoundEffectInstance, float> ApplyLowPassFilterAction = typeof(SoundEffectInstance)
        .GetMethod("INTERNAL_applyLowPassFilter", BindingFlags.Instance | BindingFlags.NonPublic)
        .CreateDelegate<Action<SoundEffectInstance, float>>();

    void ILoadable.Load(Mod mod) { }

    void ILoadable.Unload() { }

    void IAudioFilter.Apply(SoundEffectInstance instance, in AudioParameters parameters) {
        var intensity = parameters.LowPass;

        if (intensity <= 0f || instance?.IsDisposed == true) {
            return;
        }

        ApplyLowPassFilterAction?.Invoke(instance, 1f - intensity);
    }
}
