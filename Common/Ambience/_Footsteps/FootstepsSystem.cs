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
            UpdateImpact();
            UpdateFootsteps();
        }

        private void UpdateLegs() {
            var frame = Player.legFrame.Y / Player.legFrame.Height;

            if (frame != 0 || Player.IsGrounded()) {
                return;
            }

            type = LegType.Left;
        }

        private void UpdateImpact() {
            var isGrounded = Player.IsGrounded();
            var wasGrounded = Player.WasGrounded();

            var justLanded = isGrounded && !wasGrounded;
            var justJumped = !isGrounded && wasGrounded;

            if (!justLanded && !justJumped) {
                return;
            }

            var position = justLanded ? Player.Bottom : Player.oldPosition + new Vector2(Player.width / 2f, Player.height);
            var tile = Framing.GetTileSafely(position);

            if (!tile.HasTile) {
                return;
            }

            var hasMaterial = tile.TryGetMaterial(out var materialName);
            var hasFootstep = footsteps.TryGetValue(materialName, out var sound);

            SoundEngine.PlaySound(in sound, Player.Bottom);
        }

        private void UpdateFootsteps() {
            if (!Player.IsGrounded()) {
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

            var frame = Player.legFrame.Y / Player.legFrame.Height;
            var isWalking = (type == LegType.Left && (frame == 16 || frame == 17)) || (type == LegType.Right && (frame == 9 || frame == 10));

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

            var sound = new SoundStyle(footstep.SoundData.SoundPath, footstep.SoundData.Variants, SoundType.Ambient) {
                Volume = 0.2f,
                SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
            };

            footsteps[footstep.Material] = sound;
        }
    }
}
