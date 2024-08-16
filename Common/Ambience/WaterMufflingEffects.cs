using Aurora.Common.Audio;
using Aurora.Utilities.Extensions;

namespace Aurora.Common.Ambience;

/// <summary>
///     Handles water audio muffling effects for the player through an <see cref="AudioModifier" />
///     instance registered in <see cref="AudioManager" />.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class WaterMufflingEffects : ModPlayer
{
    /// <summary>
    ///     The audio muffling intensity.
    /// </summary>
    /// <remarks>
    ///     This will range from <c>0f</c> (None) - <c>0.9f</c> (Full).
    /// </remarks>
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
        if (Player.IsDrowning()) {
            Intensity += 0.05f;
        }
        else {
            Intensity -= 0.05f;
        }
    }

    private void UpdateModifier() {
        if (Intensity < 0f) {
            return;
        }
        
        AudioManager.AddModifier(
            $"{nameof(Aurora)}:{nameof(WaterMufflingEffects)}",
            60,
            (ref AudioParameters parameters, float progress) => { parameters.LowPass = Intensity * progress; }
        );
    }
}
