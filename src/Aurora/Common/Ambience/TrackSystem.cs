using System.Collections.Generic;
using System.IO;
using Aurora.Core.Configuration;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class TrackSystem : ModSystem
{
    public override void PostUpdateWorld() {
        base.PostUpdateWorld();

        UpdateTracks();
    }

    private static void UpdateTracks() {
	    if (!ClientConfiguration.Instance.EnableAmbience) {
		    return;
	    }
		
        foreach (var track in ModContent.GetContent<ITrack>()) {
            var isActive = SignalsSystem.GetSignal(track.Signals);

            if (isActive) {
                track.Volume += track.StepIn;
            }
            else {
                track.Volume -= track.StepOut;
            }

            var isInstancePlaying = SoundEngine.TryGetActiveSound(track.Slot, out var instance);
            var isSoundPlaying = instance?.IsPlaying == true;
            var isTrackPlaying = isInstancePlaying && isSoundPlaying;

            if (isActive) {
                if (isTrackPlaying) {
                    instance.Volume = track.Volume;
                }
                else {
                    track.Slot = SoundEngine.PlaySound(track.Sound);
                    track.Volume = 0f;

                    SoundEngine.TryGetActiveSound(track.Slot, out instance);

                    instance.Volume = 0f;
                }
            }
            else if (isTrackPlaying) {
                if (track.Volume > 0f) {
                    instance.Volume = track.Volume;
                }
                else {
                    instance.Stop();
                    track.Slot = SlotId.Invalid;
                }
            }
        }
    }
}
