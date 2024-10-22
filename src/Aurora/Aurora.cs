using Aurora.Core.IO;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Utilities;

namespace Aurora;

/// <summary>
///     Aurora's <see cref="Mod" /> implementation.
/// </summary>
public sealed partial class Aurora : Mod
{
    public static Aurora Instance => ModContent.GetInstance<Aurora>();
}
