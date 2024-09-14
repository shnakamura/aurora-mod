using Aurora.Core.Audio;
using Aurora.Utilities;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class WaterMufflingEffects : ModPlayer
{
    /// <summary>
    ///     The low pass filter intensity. Ranges from <c>0f</c> to <c>0.9f</c>.
    /// </summary>
    public float Intensity {
        get => _intensity;
        set => _intensity = MathHelper.Clamp(value, 0f, 0.9f);
    }

    private float _intensity;

    public override void PostUpdate() {
        base.PostUpdate();

        UpdateIntensity();
        UpdateModifier();
    }

    private void UpdateIntensity() {
        if (Player.IsUnderwater()) {
            Intensity += 0.05f;
        }
        else {
            Intensity -= 0.05f;
        }
    }

    private void UpdateModifier() {
        if (Intensity <= 0f) {
            return;
        }

        AudioSystem.AddModifier(
            $"{nameof(Aurora)}:{nameof(WaterMufflingEffects)}",
            60,
            (ref AudioParameters parameters, float progress) => { parameters.LowPass = Intensity * progress; }
        );
    }
}
