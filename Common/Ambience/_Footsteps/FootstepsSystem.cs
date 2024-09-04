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
        private enum LegType
        {
            Left,
            Right
        }

        private LegType type;

        public override void PostUpdate() {
            base.PostUpdate();

            UpdateLegs();
            UpdateSounds();
        }

        private void UpdateLegs() {
            var frame = Player.legFrame.Y / Player.legFrame.Height;

            if (frame != 0 || Player.IsGrounded()) {
                return;
            }
            
            type = LegType.Left;
        }
        
        private void UpdateSounds() {
            var frame = Player.legFrame.Y / Player.legFrame.Height;

            if (Player.velocity.X == 0f || !Player.IsGrounded()) {
                return;
            }
            
            var tile = Framing.GetTileSafely(Player.Bottom);
            
            if (!tile.HasTile) {
                return;
            }
            
            var hasMaterial = tile.TryGetMaterial(out var materialName);
            var hasFootstep = footsteps.TryGetValue(materialName, out var sound);

            if (!hasMaterial || !hasFootstep) {
                return;
            }
            
            var isWalking = type == LegType.Left && (frame == 16 || frame == 17) || type == LegType.Right && (frame == 9 || frame == 10);
            
            if (!isWalking) {
                return;
            }
            
            type = type == LegType.Left ? LegType.Right : LegType.Left;
            
            SoundEngine.PlaySound(in sound, Player.Bottom);
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
            var footstep = asset.Value;

            footsteps[footstep.Material] = footstep.Sound with {
                Variants = new ReadOnlySpan<int>(footstep.Variants),
                VariantsWeights = new ReadOnlySpan<float>(footstep.Variants)
            };
        }
    }
}
