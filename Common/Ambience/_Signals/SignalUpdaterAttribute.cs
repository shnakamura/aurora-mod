namespace Aurora.Common.Ambience._Signals;

public sealed class SignalUpdaterAttribute : Attribute
{
    public readonly string? Name;

    public SignalUpdaterAttribute(string? name = null) {
        Name = name;
    }
}
