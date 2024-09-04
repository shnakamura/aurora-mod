using Terraria.Audio;

namespace Aurora.Common.Ambience;

/// <summary>
///     Handles water audio splash effects for the player through a <see cref="SoundStyle" /> which plays everytime the player enters/leaves any water source.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class WaterSplashEffects : ModPlayer
{
    /// <summary>
    ///     The sound for the water splash.
    /// </summary>
    public static readonly SoundStyle WaterSplashSound = new($"{nameof(Aurora)}/Assets/Sounds/Ambience/WaterSplash") {
        PitchVariance = 0.25f
    };

    public override void PostUpdate() {
        base.PostUpdate();

        // The game sets Player.wetCount to 10 whenever the player exits/enters water.
        // We check for 5 to make the splash play midway through the movement.     
        if (Player.wetCount != 5) {
            return;
        }

        SoundEngine.PlaySound(in WaterSplashSound, Player.Center);
    }
}
