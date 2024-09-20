namespace Aurora.Core.Audio;

public struct AudioModifier
{
    public delegate void ModifierCallback(ref AudioParameters parameters, float progress);

    /// <summary>
    ///		The callback of this modifier. Use this to modify a provided <see cref="AudioParameters"/>
    ///		instance.
    /// </summary>
    public ModifierCallback? Callback;

    /// <summary>
    ///		The remaining time of this modifier in ticks.
    /// </summary>
    public int TimeLeft;

    /// <summary>
    ///		The max time of this modifier in ticks.
    /// </summary>
    public int TimeMax;

    /// <summary>
    ///		The unique identifier of this modifier.
    /// </summary>
    public readonly string Identifier;

    public AudioModifier(string identifier, int timeLeft, ModifierCallback? callback) {
        Identifier = identifier;
        TimeLeft = timeLeft;
        TimeMax = timeLeft;
        Callback = callback;
    }
}
