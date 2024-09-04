namespace Aurora.Common.Ambience;

public sealed class SignalUpdaterAttribute : Attribute
{
    public readonly string? Name;

    public SignalUpdaterAttribute(string? name = null) {
        Name = name;
    }
}
