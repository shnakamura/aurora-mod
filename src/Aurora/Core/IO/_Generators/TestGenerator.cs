using System.IO;
using Microsoft.CodeAnalysis;

namespace Aurora.Core.IO;

[Generator(LanguageNames.CSharp)]
internal sealed class TestGenerator : IIncrementalGenerator
{
	public const string Extension = ".ambience.footstep";
	
	public void Initialize(IncrementalGeneratorInitializationContext context) {
		var files = context.AdditionalTextsProvider;
		var data = files.Where(file => Path.GetExtension(file.Path) == Extension);
		
		context.RegisterSourceOutput(data,
			static (context, file) => {
				context.AddSource(file.Path, "public sealed class " + Path.GetFileNameWithoutExtension(file.Path) + ";");
			}
		);
	}
}
