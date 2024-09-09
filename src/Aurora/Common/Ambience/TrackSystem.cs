using System.Collections.Generic;
using System.IO;
using Aurora.Core.Configuration;
using JetBrains.Annotations;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class TrackSystem : ModSystem
{
    /// <summary>
    ///     The list of registered <see cref="Track" /> instances.
    /// </summary>
    public static List<ITrack> Tracks { get; private set; } = new();

    public override void PostSetupContent() {
        base.PostSetupContent();

    }

    public override void Unload() {
        base.Unload();

        Tracks?.Clear();
        Tracks = null;
    }

    public override void PostUpdateWorld() {
        base.PostUpdateWorld();

        UpdateTracks();
    }

    private static void UpdateTracks() {
	    if (!ClientConfiguration.Instance.EnableAmbience) {
		    return;
	    }
		
        for (var i = 0; i < Tracks.Count; i++) {
            var track = Tracks[i];

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
