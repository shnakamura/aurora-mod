using System.Collections.Generic;
using System.IO;
using Aurora.Core.IO;
using Aurora.Utilities;
using ReLogic.Content;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class FootstepsSystem : ModSystem
{
    private sealed class FootstepsSystemPlayerImpl : ModPlayer
    {
        public override void PostUpdate() {
            base.PostUpdate();

            var tile = Framing.GetTileSafely(Player.Bottom);

            if (!tile.HasTile || !tile.TryGetMaterial(out var materialName)) {
                return;
            }
            
            Main.NewText(materialName);
        }
    }
 
    public static List<Footstep> Footsteps { get; private set; } = new();
    
    public override void PostSetupContent() {
        base.PostSetupContent();
        
        LoadFootsteps(Mod);
    }

    public override void Unload() {
        base.Unload();
        
        Footsteps?.Clear();
        Footsteps = null;
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

            Footsteps.Add(asset.Value);
        }
    }
}
