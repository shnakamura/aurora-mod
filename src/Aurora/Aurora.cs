using Aurora.Core.IO;
using JetBrains.Annotations;
using ReLogic.Content.Sources;

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

    public override IContentSource CreateDefaultContentSource() {
	    var source = new SmartContentSource(base.CreateDefaultContentSource());
	    
	    source.AddRedirect("Content", "Assets/Textures");

	    return source;
    }
}
