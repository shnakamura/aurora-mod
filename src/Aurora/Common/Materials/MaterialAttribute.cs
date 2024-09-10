namespace Aurora.Common.Materials;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MaterialAttribute(string name) : Attribute
{
	/// <summary>
	///		The name of the material associated with this attribute's type.
	/// </summary>
    public readonly string Name = name;
}
