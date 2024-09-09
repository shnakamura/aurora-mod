namespace Aurora.Common.Materials;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MaterialAttribute : Attribute
{
	/// <summary>
	///		The name of the material associated with this attribute's type.
	/// </summary>
    public readonly string Name;

    public MaterialAttribute(string name) {
        Name = name;
    }
}
