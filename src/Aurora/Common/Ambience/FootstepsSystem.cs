using System.Collections.Generic;
using Aurora.Common.Materials;
using Aurora.Core.Configuration;
using Aurora.Utilities;
using JetBrains.Annotations;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
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
            
            if (!ClientConfiguration.Instance.EnableFootsteps) {
	            return;
            }
            
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
            
            PlayTileFootstep(tile);
        }

        private void UpdateFootsteps() {
            if (!Player.IsGrounded()) {
                return;
            }

            var frame = Player.legFrame.Y / Player.legFrame.Height;
            var isWalking = (type == LegType.Left && (frame == 16 || frame == 17)) || (type == LegType.Right && (frame == 9 || frame == 10));

            if (!isWalking) {
                return;
            }
            
            var tile = Framing.GetTileSafely(Player.Bottom);

            if (!tile.HasTile) {
                return;
            }
            
            type = type == LegType.Left ? LegType.Right : LegType.Left;
            
            PlayTileFootstep(tile);
        }

        private void PlayTileFootstep(Tile tile) {
            var hasMaterial = TileMaterialSystem.TryGetMaterial(tile, out var materialName);
            var hasFootstep = footsteps.TryGetValue(materialName, out var sound);

            if (!hasMaterial || !hasFootstep) {
                return;
            }
            
            SoundEngine.PlaySound(in sound, Player.Bottom);
        }
    }

    private static Dictionary<string, SoundStyle>? footsteps = new();

    public override void PostSetupContent() {
        base.PostSetupContent();

        foreach (var footstep in ModContent.GetContent<IFootstep>()) {
	        footsteps[footstep.Material] = footstep.Sound;
        }
    }

    public override void Unload() {
        base.Unload();

        footsteps?.Clear();
        footsteps = null;
    }
}
