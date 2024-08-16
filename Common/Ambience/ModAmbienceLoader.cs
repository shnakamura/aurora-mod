using System.Collections.Generic;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

/// <summary>
///     Handles registration and updating of registered <see cref="ModAmbience"/> instances.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class ModAmbienceLoader : ModSystem
{
    /// <summary>
    ///     The list of registered <see cref="ModAmbience"/> instances.
    /// </summary>
    /// <remarks>
    ///     This is automatically populated by <see cref="ModAmbience"/> inside of <see cref="ModAmbience.Register"/>,
    ///     and normally will not require any manual logic.
    /// </remarks>
    public static List<ModAmbience> Ambience { get; private set; } = new();
    
    public override void Unload() {
        base.Unload();

        Ambience?.Clear();
        Ambience = null;
    }
    
    public override void PostUpdateWorld() {
        base.PostUpdateWorld();

        UpdateTracks();
    }

    private static void UpdateTracks() {
        for (var i = 0; i < Ambience.Count; i++) {
            var track = Ambience[i];

            var isActive = track.IsActive(Main.LocalPlayer, Main.SceneMetrics);
            
            if (isActive) {
                track.Volume += track.StepIn;
            }
            else {
                track.Volume -= track.StepOut;
            }

            var isInstancePlaying = SoundEngine.TryGetActiveSound(track.Slot, out var sound);
            var isSoundPlaying = sound?.IsPlaying == true;

            var isTrackPlaying = isInstancePlaying && isSoundPlaying;

            if (isActive) {
                if (isTrackPlaying) {
                    sound.Volume = track.Volume;
                }
                else {
                    track.Slot = SoundEngine.PlaySound(track.Sound);
                    track.Volume = 0f;
                
                    SoundEngine.TryGetActiveSound(track.Slot, out sound);

                    sound.Volume = 0f;
                }
            }
            else if (isTrackPlaying) {
                if (track.Volume > 0f) {
                    sound.Volume = track.Volume;
                }
                else {
                    sound.Stop();
                    track.Slot = SlotId.Invalid;
                }
            }
        }
    }
}
