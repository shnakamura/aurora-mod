using System.Collections.Generic;
using System.IO;
using Aurora.Common.Ambience._Signals;
using Aurora.Core.IO;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class AmbienceTrackSystem : ModSystem
{
    /// <summary>
    ///     The list of registered <see cref="AmbienceTrack"/> instances.
    /// </summary>
    public static List<IAmbienceTrack> Tracks { get; private set; } = new();
    
    public override void Load() {
        base.Load();

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

            if (extension == null || extension != AmbienceTrackReader.Extension) {
                continue;
            }

            var pathWithoutExtension = Path.ChangeExtension(path, null);
            var asset = mod.Assets.Request<AmbienceTrack>(pathWithoutExtension, AssetRequestMode.ImmediateLoad);
            
            Tracks.Add(asset.Value);
        }
    }

    private static void UpdateTracks() {
        for (var i = 0; i < Tracks.Count; i++) {
            var track = Tracks[i];

            var isActive = SignalsSystem.GetSignal(track.Flags);
            
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
