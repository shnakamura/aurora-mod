using Terraria.Audio;

namespace Aurora.Common.Ambience.Tracks;

public sealed class CricketsTrack : ModAmbience
{
    public override SoundStyle Sound { get; } = new SoundStyle($"{nameof(Aurora)}/Assets/Sounds/Ambience/ForestCricketsLoop", SoundType.Ambient) {
        IsLooped = true,
        Volume = 0.7f
    };

    public override bool IsActive(Player player, SceneMetrics metrics) {
        return player.ZonePurity && !Main.dayTime;
    }
}
