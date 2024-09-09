namespace Aurora.Core.Audio;

public struct AudioModifier
{
    public delegate void ModifierCallback(ref AudioParameters parameters, float progress);

    public ModifierCallback? Callback;

    public int TimeLeft { get; set; }

    public int TimeMax { get; set; }

    public readonly string Identifier;

    public AudioModifier(string identifier, int timeLeft, ModifierCallback? callback) {
        Identifier = identifier;
        TimeLeft = timeLeft;
        TimeMax = timeLeft;
        Callback = callback;
    }
}
