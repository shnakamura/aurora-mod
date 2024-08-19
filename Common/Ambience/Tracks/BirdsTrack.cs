using Terraria.Audio;

namespace Aurora.Common.Ambience.Tracks;

public sealed class BirdsTrack : ModAmbience
{
    public override SoundStyle Sound { get; } = new SoundStyle($"{nameof(Aurora)}/Assets/Sounds/Ambience/ForestBirdsLoop", SoundType.Ambient) {
        IsLooped = true,
        Volume = 0.7f
    };

    public override bool IsActive(Player player, SceneMetrics metrics) {
        return player.ZonePurity && Main.dayTime;
    }
}
