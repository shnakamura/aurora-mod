namespace Aurora.Common.Audio;

/// <summary>
///     Provides an audio modifier which is responsible for
///     manipulating an <see cref="AudioParameters" /> instance.
/// </summary>
public struct AudioModifier
{
    public delegate void ModifierCallback(ref AudioParameters parameters, float progress);

    /// <summary>
    ///     The modifier callback of this modifier.
    /// </summary>
    /// <remarks>
    ///     Use this to manipulate the provided <see cref="AudioParameters" /> instance.
    /// </remarks>
    public ModifierCallback? Callback;

    /// <summary>
    ///     The duration left of this modifier in ticks.
    /// </summary>
    public int TimeLeft { get; set; }

    /// <summary>
    ///     The max duration of this modifier in ticks.
    /// </summary>
    public int TimeMax { get; set; }

    /// <summary>
    ///     The unique identifier of this modifier.
    /// </summary>
    public readonly string Identifier;

    public AudioModifier(string identifier, int timeLeft, ModifierCallback? callback) {
        Identifier = identifier;
        TimeLeft = timeLeft;
        TimeMax = timeLeft;
        Callback = callback;
    }
}
