using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

/// <summary>
///     Provides a <see cref="ModType"/> which will behave as an ambience track.
/// </summary>
[Autoload(Side = ModSide.Client)]
public abstract class ModAmbience : ModType
{
    /// <summary>
    ///     The volume of this ambience track.
    /// </summary>
    /// <remarks>
    ///     This will range from <c>0f</c> (None) - <c>1f</c> (Full).
    /// </remarks>
    public float Volume {
        get => _volume;
        set => _volume = MathHelper.Clamp(value, 0f, 1f);
    }
    
    /// <summary>
    ///     The sound slot of this ambience track.
    /// </summary>
    public SlotId Slot { get; internal set; }
    
    /// <summary>
    ///     The step amount of this ambience track during fade-ins.
    /// </summary>
    /// <remarks>
    ///     This value will be applied to the track's volume every tick while its fading in.
    /// </remarks>
    public virtual float StepIn { get; } = 0.01f;

    /// <summary>
    ///     The step amount of this ambience track during fade-outs.
    /// </summary>
    /// <remarks>
    ///     This value will be applied to the track's volume every tick while its fading out.
    /// </remarks>
    public virtual float StepOut { get; } = 0.01f;
    
    /// <summary>
    ///     The sound style of this ambience track.
    /// </summary>
    /// <remarks>
    ///     This <see cref="SoundStyle"/> requires <see cref="SoundStyle.IsLooped"/> to be set as <c>true</c>.
    /// </remarks>
    public abstract SoundStyle Sound { get; }
    
    private float _volume;

    protected sealed override void Register() {
        ModAmbienceLoader.Ambience.Add(this);
        
        ModTypeLookup<ModAmbience>.Register(this);
    }

    /// <summary>
    ///     Whether this ambience track should be active or not.
    /// </summary>
    /// <param name="player">The provided player.</param>
    /// <param name="metrics">The provided scene metrics.</param>
    /// <returns><c>true</c> if the ambience track should be active; otherwise, <c>false</c>.</returns>
    public abstract bool IsActive(Player player, SceneMetrics metrics);
}
