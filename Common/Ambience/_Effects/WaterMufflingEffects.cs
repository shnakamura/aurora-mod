using Aurora.Core.Audio;
using Aurora.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

/// <summary>
///     Handles water audio muffling effects for the player through an <see cref="AudioModifier" />
///     instance registered in <see cref="AudioSystem" />.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class WaterMufflingEffects : ModPlayer
{
    /// <summary>
    ///     The audio muffling intensity. Ranges from <c>0f</c> to <c>0.9f</c>.
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
        if (Player.IsDrowning()) {
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