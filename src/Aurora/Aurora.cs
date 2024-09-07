using JetBrains.Annotations;

namespace Aurora;

/// <summary>
///     Aurora's <see cref="Mod" /> implementation.
/// </summary>
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class Aurora : Mod
{
    /// <summary>
    ///     Aurora's <see cref="Mod" /> instance.
    /// </summary>
    public static Aurora Instance => ModContent.GetInstance<Aurora>();
}
