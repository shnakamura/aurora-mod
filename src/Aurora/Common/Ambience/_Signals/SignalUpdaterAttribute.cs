namespace Aurora.Common.Ambience;

[AttributeUsage(AttributeTargets.Method)]
public sealed class SignalUpdaterAttribute : Attribute
{
	/// <summary>
	///		The name of this attribute's signal.
	/// </summary>
    public readonly string? Name;

    public SignalUpdaterAttribute(string? name = null) {
        Name = name;
    }
}
