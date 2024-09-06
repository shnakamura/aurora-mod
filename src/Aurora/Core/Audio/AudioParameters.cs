namespace Aurora.Core.Audio;

/// <summary>
///     A container with all audio parameters supported by <see cref="AudioSystem" />.
/// </summary>
public struct AudioParameters
{
    private float _lowPass;

    public float LowPass {
        get => _lowPass;
        set => _lowPass = MathHelper.Clamp(value, 0f, 1f);
    }
}
