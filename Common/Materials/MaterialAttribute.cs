namespace Aurora.Common.Materials;

public sealed class MaterialAttribute : Attribute
{
    public readonly string Name;

    public MaterialAttribute(string name) {
        Name = name;
    }
}
