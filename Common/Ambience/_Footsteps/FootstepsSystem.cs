using System.Collections.Generic;
using System.IO;
using Aurora.Core.IO;
using Aurora.Utilities;
using ReLogic.Content;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class FootstepsSystem : ModSystem
{
    private sealed class FootstepsSystemPlayerImpl : ModPlayer
    {
        public override void PostUpdate() {
            base.PostUpdate();

            var tile = Framing.GetTileSafely(Player.Bottom);

            var hasMaterial = tile.TryGetMaterial(out var materialName);
            var hasFootstep = footsteps.TryGetValue(materialName, out var sound);

            if (!tile.HasTile || !hasMaterial || !hasFootstep) {
                return;
            }

            SoundEngine.PlaySound(in sound, Player.Center);
        }
    }

    private static Dictionary<string, SoundStyle> footsteps = new();

    public override void PostSetupContent() {
        base.PostSetupContent();

        LoadFootsteps(Mod);
    }

    public override void Unload() {
        base.Unload();

        footsteps?.Clear();
        footsteps = null;
    }

    public static void LoadFootsteps(Mod mod) {
        var files = mod.RootContentSource.EnumerateAssets();

        foreach (var path in files) {
            var extension = Path.GetExtension(path);

            if (extension == null || extension != FootstepReader.Extension) {
                continue;
            }

            var pathWithoutExtension = Path.ChangeExtension(path, null);
            var asset = mod.Assets.Request<Footstep>(pathWithoutExtension, AssetRequestMode.ImmediateLoad);

            footsteps[asset.Value.Material] = asset.Value.Sound;
        }
    }
}
