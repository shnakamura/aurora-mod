using System.Collections.Generic;
using System.IO;
using Aurora.Common.Configuration;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class AmbienceTrackSystem : ModSystem
{
    /// <summary>
    ///     The list of registered <see cref="AmbienceTrack" /> instances.
    /// </summary>
    public static List<IAmbienceTrack> Tracks { get; private set; } = new();

    public override void PostSetupContent() {
        base.PostSetupContent();

        LoadTracks(Mod);
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

    public static void LoadTracks(Mod mod) {
        var files = mod.RootContentSource.EnumerateAssets();

        foreach (var path in files) {
            var extension = Path.GetExtension(path);

            if (extension == null || extension != IAmbienceTrack.Extension) {
                continue;
            }

            var pathWithoutExtension = Path.ChangeExtension(path, null);

            var asset = mod.Assets.Request<AmbienceTrack>(pathWithoutExtension, AssetRequestMode.ImmediateLoad);
            var track = asset.Value;

            Tracks.Add(track);
        }
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
                    var sound = new SoundStyle(track.SoundData.SoundPath, SoundType.Ambient) {
                        Volume = 0.8f,
                        IsLooped = true
                    };
                    
                    track.Slot = SoundEngine.PlaySound(in sound);
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
