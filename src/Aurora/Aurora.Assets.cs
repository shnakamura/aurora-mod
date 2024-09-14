using Aurora.Core.IO;
using Aurora.Utilities;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Utilities;

namespace Aurora;

public sealed partial class Aurora : Mod
{
	public override IContentSource CreateDefaultContentSource() {
		// Asset readers must be registered before overriding the default content source.
		if (!Main.dedServ) {
			AddContent(new OgvReader());
		}
		
		var source = new SmartContentSource(base.CreateDefaultContentSource());
	    
		source.AddRedirect("Content", "Assets/Textures");

		return source;
	}
}
