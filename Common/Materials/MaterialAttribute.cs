namespace Aurora.Common.Materials;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MaterialAttribute : Attribute
{
	/// <summary>
	///		The name of this attribute's material.
	/// </summary>
    public readonly string Name;

    public MaterialAttribute(string name) {
        Name = name;
    }
}
