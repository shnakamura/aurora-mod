using Terraria.Audio;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class WaterSplashEffects : ModPlayer
{
    public static readonly SoundStyle WaterSplashSound = new($"{nameof(Aurora)}/Assets/Sounds/Ambience/WaterSplash") {
        PitchVariance = 0.25f
    };

    public override void PostUpdate() {
        base.PostUpdate();

        if (Player.wetCount != 5) {
            return;
        }

        SoundEngine.PlaySound(in WaterSplashSound, Player.Center);
    }
}
